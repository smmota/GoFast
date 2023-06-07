using GoFast.API;
using GoFast.API.Data;
using GoFast.API.Data.Repositories;
using GoFast.API.Interfaces.Repositories;
using GoFast.API.Models;
using GoFast.API.Models.ViewModels;
using GoFast.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("database");
var connBlobStorage = builder.Configuration.GetConnectionString("connBlobStorage");
var key = Encoding.ASCII.GetBytes(Settings.Secret);

builder.Services.AddDbContext<SqlContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddTransient<IMotoristaRepository, MotoristaRepository>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<IBlobStorageRepository, BlobStorageRepository>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("admin"));
    options.AddPolicy("User", policy => policy.RequireRole("user"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

#region Usuario

//app.MapPost("/login", async (
//    SqlContext context,
//    Login model) =>
//    await context.Usuario.Where(x => x.LoginUser == model.LoginUser && x.Senha == model.Senha).FirstAsync()
//        is Usuario user 
//        ? Results.Ok(new
//            {
//                usuario = user,
//                token = TokenService.GenerateToken(user)
//            })
//        : Results.NotFound(new
//        {
//            message = "Usuário ou senha incorreto!"
//        }))
//    .WithName("Login")
//    .WithTags("Usuario");

app.MapPost("/v1/cadastrarUsuario", (IUsuarioRepository usuarioRepository, UsuarioViewModel model) =>
{
    var hash = new Hash(SHA512.Create());
    Guid guid = Guid.NewGuid();
    var senhaCripto = hash.CriptografarSenha(model.Senha + guid.ToString());

    Usuario usuario = new Usuario()
    {
        Id = guid,
        Nome = model.Nome,
        LoginUser = model.LoginUser,
        Senha = model.Senha,
        Ativo = true,
        Role = "user"
    };

    usuarioRepository.Add(usuario);

    return Results.Ok(new
    {
        message = $"Usuario {usuario.Nome} cadastrado com sucesso!"
    });
}).AllowAnonymous().WithTags("Usuario");

app.MapPost("/v1/login", (IUsuarioRepository usuarioRepository, LoginViewModel model) =>
{
    if (string.IsNullOrEmpty(model.LoginUser) || string.IsNullOrEmpty(model.Senha))
        return Results.NotFound(new
        {
            message = "Informe o usuário e senha!"
        });

    var usuario = usuarioRepository.GetUsuarioByUserAndPassword(model.LoginUser, model.Senha);

    if (usuario == null)
        return Results.NotFound(new
        {
            message = "Usuário ou senha incorreto!"
        });

    if (!usuario.Ativo)
        return Results.Ok(new
        {
            message = "Usuário bloqueado!"
        });

    var token = TokenService.GenerateToken(usuario);

    usuario.Senha = "";

    return Results.Ok(new
    {
        usuario = usuario,
        token = token
    });
}).WithTags("Usuario");

#endregion

#region Blob Storage

app.MapPost("/v1/uploadImage", (IBlobStorageRepository blobStorageRepository, UploadImageViewModel model, ClaimsPrincipal user) =>
{
    if (string.IsNullOrEmpty(model.Imagem))
        return Results.BadRequest(new
        {
            message = "O valor a imagem deve ser preenchido!"
        });

    try
    {
        var container = "data";
        var uploadService = new FileUpload(connBlobStorage);
        var blobClient = uploadService.UploadBase64Image(model.Imagem, container);

        try
        {
            BlobStorage blobStorage = new BlobStorage()
            {
                Name = blobClient.Name,
                Link = blobClient.Uri.AbsoluteUri,
                base64 = "Teste",
                Container = blobClient.BlobContainerName,
                IdUsuario = user.Identity.Name,
                IdAzure = "Teste"
            };

            blobStorageRepository.Add(blobStorage);

            return Results.Ok(new
            {
                urlImagem = blobClient.Uri.AbsoluteUri
            });
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new
            {
                message = $"Erro ao salvar as informações no banco de dados. {ex.Message}"
            });
        }
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new
        {
            message = $"Erro ao fazer upload da imagem. {ex.Message}"
        });
    }

}).RequireAuthorization().WithTags("Blob Storage"); //.RequireAuthorization("Admin");

app.MapPost("/v1/deleteImage", (IBlobStorageRepository blobStorageRepository, DeleteImageViewModel model, ClaimsPrincipal user) =>
{
    if (string.IsNullOrEmpty(model.urlImagem))
        return Results.BadRequest(new
        {
            message = "A url deve ser preenchida!"
        });

    var blobStorageList = blobStorageRepository.GetByIdUsuario(user.Identity.Name);
    var blobStorage = blobStorageList.Where(x => x.Link == model.urlImagem).FirstOrDefault();

    if (blobStorage == null)
        return Results.NotFound(new
        {
            message = "A imagem não foi localizada!"
        });

    var uploadService = new FileUpload(connBlobStorage);
    bool result = uploadService.DeleteImage(blobStorage);

    if (!result)
        return Results.BadRequest(new
        {
            message = "Não foi possível remover o arquivo informado!"
        });

    blobStorageRepository.Remove(blobStorage.Id);

    return Results.Ok(new
    {
        message = "Arquivo removido com sucesso!"
    });
}).RequireAuthorization().WithTags("Blob Storage"); //.RequireAuthorization("Admin");

#endregion

#region Motorista

app.MapGet("v1/Motorista", (IMotoristaRepository motoristaRepository) =>
{
    return Results.Ok(motoristaRepository.GetAll());
}).WithTags("Motorista");

app.MapPost("v1/Motorista", (
    IMotoristaRepository motoristaRepository,
    MotoristaViewModel model) =>
{
    var motorista = model.MapTo();

    motoristaRepository.Add(motorista);

    return Results.Ok();
}).WithTags("Motorista");

app.MapDelete("v1/Motorista", (
    IMotoristaRepository motoristaRepository,
    Guid idMotorista) =>
{
    motoristaRepository.Remove(idMotorista);

    return Results.Ok();
}).WithTags("Motorista");

app.MapPut("v1/Motorista", (
    IMotoristaRepository motoristaRepository,
    MotoristaViewModel model) =>
{
    var motorista = model.MapTo();

    //motoristaRepository.Update(motorista);

    return Results.Ok();
}).WithTags("Motorista");

#endregion


app.Run();