using AutoMapper;
using Azure.Storage.Blobs;
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
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("database");
var connectionStringIdentity = builder.Configuration.GetConnectionString("identityDB");
var key = Encoding.ASCII.GetBytes(Settings.Secret);

builder.Services.AddDbContext<SqlContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionStringIdentity));

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
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("admin"));
    options.AddPolicy("User", policy => policy.RequireRole("user"));
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiGoFast", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = @"JWT Authorization header usando o schema Bearer
                       \r\n\r\n Informe 'Bearer'[space].
                        Exemplo: \'Bearer 12345abcdef\'",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });

    c.ResolveConflictingActions(x => x.First());
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


#region Usuario

app.MapPost("/api/Usuario/Create", async (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, UsuarioViewModel model) =>
{
    List<string> erros = new List<string>();

    if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
    {
        erros.Add("Informe o usuário e senha!");

        return Results.BadRequest(new
        {
            Successful = false,
            Errors = erros
        });
    }

    if(model.Password != model.ConfirmPassword)
    {
        erros.Add("As senhas informadas não conferem!");

        return Results.BadRequest(new
        {
            Successful = false,
            Errors = erros
        });
    }

    var user = new ApplicationUser { UserName = model.Email, Email = model.Email, NormalizedUserName = model.Email };
    var result = await userManager.CreateAsync(user, model.Password);

    if (!result.Succeeded)
    {
        foreach (var erro in result.Errors)
            erros.Add(erro.Description);

        return Results.BadRequest(new
        {
            Successful = false,
            Errors = erros
        });
    }

    return Results.Ok(new
    {
        Successful = true,
        Errors = $"Usuario {model.Email} cadastrado com sucesso!"
    });
})
    .AllowAnonymous()
    .WithTags("Usuario");

app.MapPost("/api/Usuario/login", async (SignInManager<ApplicationUser> signInManager, LoginViewModel model) =>
{
    if (string.IsNullOrEmpty(model.LoginUser) || string.IsNullOrEmpty(model.Senha))
        return Results.NotFound(new LoginErroViewModel
        {
            Error = "Informe o usuário e senha!"
        });

    var result = await signInManager.PasswordSignInAsync(model.LoginUser, model.Senha, isPersistent: false, lockoutOnFailure: false);

    //if (!result.Succeeded)
    //    return Results.NotFound(new
    //    {
    //        message = "Usuário ou senha inválido!"
    //    });

    if (!result.Succeeded)
        return Results.NotFound(new LoginErroViewModel
        {
            Error = "Usuário ou senha inválido!"
        });

    if (result.IsLockedOut)
        return Results.Ok(new LoginErroViewModel
        {
            Error = "Usuário bloqueado!"
        });

    if (result.IsNotAllowed)
        return Results.Unauthorized();

    var token = TokenService.BuildToken(model);

    return Results.Ok(new
    {
        token.Token,
        token.Expiration
    });
})
    .AllowAnonymous()
    .WithTags("Usuario");

#endregion

#region Blob Storage

app.MapPost("/api/Imagem/Upload", async (IBlobStorageRepository blobStorageRepository, IBlobStorageService blobStorageService, UploadImageViewModel model) =>
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
                Container = blobClient.BlobContainerName,
            };

            await blobStorageRepository.Add(blobStorage);

            return Results.Ok(new
            {
                id = blobStorage.Id,
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

})
    .RequireAuthorization()
    .WithTags("Imagem");

app.MapPost("/api/Imaggem/Delete", async (IBlobStorageRepository blobStorageRepository, IBlobStorageService blobStorageService, DeleteImageViewModel model) =>
{
    if (model.IdBlob == null)
        return Results.BadRequest(new
        {
            message = "O id da imagem deve ser informado!"
        });

    var blobStorage = await blobStorageRepository.GetById(model.IdBlob);

    if (blobStorage == null)
        return Results.NotFound(new
        {
            message = "A imagem não foi localizada!"
        });

    if (!await blobStorageService.DeleteImage(blobStorage))
        return Results.BadRequest(new
        {
            message = "Não foi possível remover o arquivo informado!"
        });

    await blobStorageRepository.Remove(blobStorage.Id);

    return Results.Ok(new
    {
        message = "Arquivo removido com sucesso!"
    });
})
    .RequireAuthorization()
    .WithTags("Imagem");

app.MapGet("/api/Imagem/GetById", async (IBlobStorageRepository blobStorageRepository, IMapper _mapper, Guid idBlobStorage) =>
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
})
    //.RequireAuthorization()
    .WithName("GetImageById")
    .WithTags("Imagem");

#endregion

#region Motorista

app.MapGet("/api/Motorista/GetAll", async (IMotoristaRepository motoristaRepository, IMapper _mapper) =>
 {
     var motoristaDM = await motoristaRepository.GetAllMotoristas();

     if (motoristaDM != null)
        return Results.Ok(_mapper.Map<List<MotoristaViewModel>>(motoristaDM));

    return Results.NoContent();
})
    .RequireAuthorization()
    .WithTags("Motorista");

app.MapGet("/api/Motorista/GetById", async (IMotoristaRepository motoristaRepository, IMapper _mapper, Guid idMotorista) =>
{
    var motoristaDM = await motoristaRepository.GetMotoristaById(idMotorista);

    if (motoristaDM != null)
        return Results.Ok(_mapper.Map<MotoristaViewModel>(motoristaDM));

    return Results.NoContent();
})
    //.RequireAuthorization()
    .WithTags("Motorista");

app.MapPost("/api/Motorista/Create", async (IBlobStorageRepository blobStorageRepository, IBlobStorageService blobStorageService, IMotoristaRepository motoristaRepository, IMapper _mapper, MotoristaInputModel model) =>
{
    if (string.IsNullOrEmpty(model.Blob.ImagemBase64))
        return Results.BadRequest(new
        {
            message = "O valor a imagem deve ser preenchido!"
        });

    BlobStorage blobStorage;
    BlobClient blobClient;

    try
    {
        var container = "data";
        blobClient = await blobStorageService.UploadBase64Image(model.Blob.ImagemBase64, container);

        if (blobClient == null)
            return Results.BadRequest(new
            {
                message = "Erro ao fazer upload da imagem."
            });

        blobStorage = new BlobStorage()
        {
            Id = Guid.NewGuid(),
            Name = blobClient.Name,
            Link = blobClient.Uri.AbsoluteUri,
            Container = blobClient.BlobContainerName,
        };
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new
        {
            message = $"Erro ao fazer upload da imagem. {ex.Message}"
        });
    }

    var motoristaVM = _mapper.Map<MotoristaViewModel>(model);
    var motoristaDM = _mapper.Map<Motorista>(motoristaVM);

    motoristaDM.Id = Guid.NewGuid();
    motoristaDM.Carro.DocumentoCarro.BlobStorage = blobStorage;

    try
    {
        await motoristaRepository.Add(motoristaDM);
        
        return Results.Ok(new
        {
            motoristaDM.Id
        });
    }
    catch (Exception ex)
    {

        blobStorage = await blobStorageRepository.GetById(blobStorage.Id);

        await blobStorageService.DeleteImage(blobStorage);
        await blobStorageRepository.Remove(blobStorage.Id);

        return Results.BadRequest(new
        {
            message = $"Erro ao cadastrar o motorista. {ex.Message}"
        });
    }
})
    //.RequireAuthorization()
    .WithTags("Motorista");

app.MapDelete("/api/Motorista/Delete", async (IMotoristaRepository motoristaRepository, Guid idMotorista) =>
{
    await motoristaRepository.Remove(idMotorista);

    return Results.Ok(new
    {
        message = "Motorista excluído com sucesso!"
    });
})
    //.RequireAuthorization()
    .WithTags("Motorista");

app.MapPut("/api/Motorista/Update", async (IMotoristaRepository motoristaRepository, IMapper _mapper, MotoristaViewModel model) =>
{
    var motoristaDM = _mapper.Map<Motorista>(model);

    await motoristaRepository.Update(motoristaDM);

    return Results.Ok(new
    {
        message = "Motorista atualizado com sucesso!"
    });
})
    .RequireAuthorization()
    .WithTags("Motorista");

#endregion


app.Run();