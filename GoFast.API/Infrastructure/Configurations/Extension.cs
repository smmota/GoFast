using GoFast.API.Data.Repositories;
using GoFast.API.Interfaces.Repositories;
using GoFast.API.Interfaces.Services;
using GoFast.API.Services;

namespace GoFast.API.Infrastructure.Configurations
{
    public static class Extension
    {
        public static IServiceCollection DependencyMap(this IServiceCollection services) 
        {
            services.AddTransient<IMotoristaRepository, MotoristaRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IBlobStorageRepository, BlobStorageRepository>();

            services.AddTransient<IBlobStorageService, BlobStorageService>();
            services.AddTransient<IHashService, HashService>();

            return services;
        }   
    }       
}           
