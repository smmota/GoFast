using GoFast.API;
using GoFast.API.Data;
using GoFast.API.Data.Repositories;
using GoFast.API.Interfaces.Repositories;
using GoFast.API.Models;
using GoFast.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("database");
builder.Services.AddDbContext<SqlContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddTransient<BaseRepository<Motorista>, MotoristaRepository>();

var key = Encoding.ASCII.GetBytes(Settings.Secret);

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

app.MapPost("/login", (Login model, SqlContext context) =>
{
    if (string.IsNullOrEmpty(model.LoginUser) || string.IsNullOrEmpty(model.Senha))
        return Results.NotFound(new
        {
            message = "Informe o usuário e senha!"
        });

    UsuarioRepository usuarioRepository = new UsuarioRepository(context);
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
}).WithTags("Login");

app.MapPost("/uploadImage", (UploadImage model, ClaimsPrincipal user) =>
{
    if (string.IsNullOrEmpty(model.Image))
        return Results.BadRequest(new
        {
            message = "O valor a imagem deve ser preenchido!"
        });

    var uploadService = new FileUpload();

    var url = uploadService.UploadBase64Image(model.Image, "data");

    return Results.Ok(new
    {
        urlImage = url
    });
}).RequireAuthorization().WithTags("Blob Storage"); //.RequireAuthorization("Admin");

app.MapGet("v1/Motorista", (BaseRepository<Motorista> motoristaRepository) =>
{
    return Results.Ok(motoristaRepository.GetAll());
}).WithTags("Motorista");

app.MapPost("v1/Motorista", (
    BaseRepository<Motorista> motoristaRepository,
    MotoristaViewModel model) =>
{
    var motorista = model.MapTo();

    motoristaRepository.Add(motorista);

    return Results.Ok();
}).WithTags("Motorista");

app.MapDelete("v1/Motorista", (
    BaseRepository<Motorista> motoristaRepository,
    Guid idMotorista) =>
{
    motoristaRepository.Remove(idMotorista);

    return Results.Ok();
}).WithTags("Motorista");

app.MapPut("v1/Motorista", (
    BaseRepository<Motorista> motoristaRepository,
    MotoristaViewModel model) =>
{
    var motorista = model.MapTo();

    //motoristaRepository.Update(motorista);

    return Results.Ok();
}).WithTags("Motorista");

app.Run();