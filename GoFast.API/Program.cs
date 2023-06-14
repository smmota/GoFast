using AutoMapper;
using GoFast.API;
using GoFast.API.Data;
using GoFast.API.Data.Repositories;
using GoFast.API.Infrastructure.Configurations;
using GoFast.API.Interfaces.Repositories;
using GoFast.API.Interfaces.Services;
using GoFast.API.Models;
using GoFast.API.Models.InputModel;
using GoFast.API.Models.ViewModels;
using GoFast.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("database");
var key = Encoding.ASCII.GetBytes(Settings.Secret);

builder.Services.AddDbContext<SqlContext>(options => options.UseSqlServer(connectionString));

builder.Services.DependencyMap();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(x => x.First());
});

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
//            message = "Usu�rio ou senha incorreto!"
//        }))
//    .WithName("Login")
//    .WithTags("Usuario");

app.MapPost("/v1/CreateUsuario", async (IUsuarioRepository usuarioRepository, IHashService hashService, UsuarioViewModel model) =>
{
    if (await usuarioRepository.VerificaSeUsuarioExiste(model.LoginUser))
        return Results.Ok(new
        {
            message = "E-mail informado j� est� cadastrado!"
        });

    Guid guid = Guid.NewGuid();

    Usuario usuario = new Usuario()
    {
        Id = guid,
        Nome = model.Nome,
        LoginUser = model.LoginUser,
        Senha = hashService.CriptografarSenha(model.Senha + guid.ToString()),
        Ativo = true,
        Role = "user"
    };

    await usuarioRepository.Add(usuario);

    return Results.Ok(new
    {
        message = $"Usuario {usuario.Nome} cadastrado com sucesso!"
    });
}).AllowAnonymous().WithName("CreateUsuario").WithTags("Usuario");

app.MapPut("/v1/statusUsuario", async (IUsuarioRepository usuarioRepository, UsuarioStatusViewModel model) =>
{
    var usuario = await usuarioRepository.GetUsuarioByLogin(model.LoginUser);

    if (usuario == null)
        return Results.NotFound(new
        {
            message = "Usu�rio n�o cadastrado!"
        });

    usuario.Ativo = model.Ativo;

    await usuarioRepository.Update(usuario);

    if (!usuario.Ativo)
        return Results.Ok(new
        {
            message = "Usu�rio bloqueado com sucesso!"
        });
    else
        return Results.Ok(new
        {
            message = "Usu�rio desbloqueado com sucesso!"
        });
}).RequireAuthorization("Admin").WithName("UpdateUsuario").WithTags("Usuario");

app.MapPost("/v1/login", async (IUsuarioRepository usuarioRepository, IHashService hashService, LoginViewModel model) =>
{
    if (string.IsNullOrEmpty(model.LoginUser) || string.IsNullOrEmpty(model.Senha))
        return Results.NotFound(new
        {
            message = "Informe o usu�rio e senha!"
        });

    var usuario = await usuarioRepository.GetUsuarioByLogin(model.LoginUser);

    if (usuario == null)
        return Results.NotFound(new
        {
            message = "Usu�rio n�o cadastrado!"
        });

    var senhaDigitadaCripto = hashService.CriptografarSenha(model.Senha + usuario.Id);

    if (senhaDigitadaCripto != usuario.Senha)
        return Results.NotFound(new
        {
            message = "Usu�rio ou senha incorreto!"
        });

    if (!usuario.Ativo)
        return Results.Ok(new
        {
            message = "Usu�rio bloqueado!"
        });

    var token = TokenService.GenerateToken(usuario);

    usuario.Senha = "";

    return Results.Ok(new
    {
        usuario = usuario,
        token = token
    });
}).AllowAnonymous().WithName("Login").WithTags("Usuario");

#endregion

#region Blob Storage

app.MapPost("/v1/uploadImage", async (IBlobStorageRepository blobStorageRepository, IBlobStorageService blobStorageService, UploadImageViewModel model, ClaimsPrincipal user) =>
{
    if (string.IsNullOrEmpty(model.Imagem))
        return Results.BadRequest(new
        {
            message = "O valor a imagem deve ser preenchido!"
        });

    try
    {
        var container = "data";
        var blobClient = await blobStorageService.UploadBase64Image(model.Imagem, container);

        if(blobClient == null)
            return Results.BadRequest(new
            {
                message = "Erro ao fazer upload da imagem."
            });

        try
        {
            BlobStorage blobStorage = new BlobStorage()
            {
                Id = Guid.NewGuid(),
                Name = blobClient.Name,
                Link = blobClient.Uri.AbsoluteUri,
                base64 = "Teste",
                Container = blobClient.BlobContainerName,
                //IdUsuario = user.Claims.Where(x => x.Type.Contains("sid")).FirstOrDefault().Value,
                IdUsuario = "teste@teste.com",
                IdAzure = "Teste"
            };

            await blobStorageRepository.Add(blobStorage);

            return Results.Ok(new
            {
                id = blobStorage.Id,
                //urlImagem = blobClient.Uri.AbsoluteUri
            });
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new
            {
                message = $"Erro ao salvar as informa��es no banco de dados. {ex.Message}"
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

}).WithName("UploadImage").WithTags("Blob Storage"); //.RequireAuthorization("Admin"); .RequireAuthorization()

app.MapPost("/v1/deleteImage", async (IBlobStorageRepository blobStorageRepository, IBlobStorageService blobStorageService, DeleteImageViewModel model, ClaimsPrincipal user) =>
{
    if (model.IdBlob == null)
        return Results.BadRequest(new
        {
            message = "A url deve ser preenchida!"
        });

    var blobStorage = await blobStorageRepository.GetById(model.IdBlob);

    if (blobStorage == null)
        return Results.NotFound(new
        {
            message = "A imagem n�o foi localizada!"
        });

    bool result = await blobStorageService.DeleteImage(blobStorage);

    if (!result)
        return Results.BadRequest(new
        {
            message = "N�o foi poss�vel remover o arquivo informado!"
        });

    await blobStorageRepository.Remove(blobStorage.Id);

    return Results.Ok(new
    {
        message = "Arquivo removido com sucesso!"
    });
}).RequireAuthorization().WithName("DeleteImage").WithTags("Blob Storage"); //.RequireAuthorization("Admin");

app.MapGet("/v1/GetById", async (IBlobStorageRepository blobStorageRepository, IMapper _mapper, Guid idBlobStorage) =>
{
    if (string.IsNullOrEmpty(idBlobStorage.ToString()))
        return Results.BadRequest(new
        {
            message = "Informe o id da imagem!"
        });

    var blobStorage = await blobStorageRepository.GetById(idBlobStorage);

    if (blobStorage != null)
        return Results.Ok(_mapper.Map<BlobStorageViewModel>(blobStorage));

    return Results.NoContent();
}).WithName("GetImageById").WithTags("Blob Storage");

#endregion

#region Motorista

app.MapGet("/v1/GetAll", async (IMotoristaRepository motoristaRepository, IMapper _mapper) =>
 {
     var motoristaDM = await motoristaRepository.GetAllMotoristas();

     if (motoristaDM != null)
        return Results.Ok(_mapper.Map<List<MotoristaViewModel>>(motoristaDM));

    return Results.NoContent();
}).WithName("GetAllMotoristas").WithTags("Motorista");

app.MapGet("/v1/GetMotoristaById", async (IMotoristaRepository motoristaRepository, IMapper _mapper, Guid idMotorista) =>
{
    var motoristaDM = await motoristaRepository.GetMotoristaById(idMotorista);

    if (motoristaDM != null)
        return Results.Ok(_mapper.Map<MotoristaViewModel>(motoristaDM));

    return Results.NoContent();
}).WithName("GetMotoristaById").WithTags("Motorista");

app.MapPost("/v1/Create", async (IMotoristaRepository motoristaRepository, IMapper _mapper, MotoristaInputModel model) =>
{
    var motoristaVM = _mapper.Map<MotoristaViewModel>(model);
    var motoristaDM = _mapper.Map<Motorista>(motoristaVM);

    await motoristaRepository.Add(motoristaDM);

    return Results.Ok(new
    {
        motoristaDM.Id
    });
}).WithName("CreateMotorista").WithTags("Motorista");

app.MapDelete("/v1/DeleteById", async (
    IMotoristaRepository motoristaRepository,
    Guid idMotorista) =>
{
    await motoristaRepository.Remove(idMotorista);

    return Results.Ok();
}).WithName("DeleteMotorista").WithTags("Motorista");

app.MapPut("/v1/Update", async (
    IMotoristaRepository motoristaRepository,
    IMapper _mapper,
    MotoristaViewModel model) =>
{
    var motoristaDM = _mapper.Map<Motorista>(model);

    await motoristaRepository.Update(motoristaDM);

    return Results.Ok();
}).WithName("UpdateMotorista").WithTags("Motorista");

#endregion


app.Run();