using Autofac;
using GoFast.Application;
using GoFast.Application.Interfaces;
using GoFast.Application.Mappers;
using GoFast.Application.Mappers.Interfaces;
using GoFast.Domain.Core.Interfaces.Repositories;
using GoFast.Domain.Core.Interfaces.Services;
using GoFast.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFast.Infrastructure.CrossCutting.IOC
{
    internal class ConfigurationIOC
    {
        public static void Load(ContainerBuilder builder)
        {
            #region IOC

            #region ApplicationService

            builder.RegisterType<UsuarioApplicationService>().As<IUsuarioApplicationService>();

            #endregion


            #region Service

            builder.RegisterType<UsuarioService>().As<IUsuarioService>();

            #endregion


            #region Repository

            //builder.RegisterType<UsuarioRepository>().As<IUsuarioRepository>();

            #endregion


            #region Mapper

            builder.RegisterType<UsuarioMapper>().As<IUsuarioMapper>();

            #endregion

            #endregion
        }
    }
}
