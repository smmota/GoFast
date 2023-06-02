using GoFast.Domain.Core.Interfaces.Repositories;
using GoFast.Domain.Core.Interfaces.Services;
using GoFast.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFast.Domain.Services
{
    public class UsuarioService : BaseService<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository) : base(usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public Usuario GetUsuarioByUserAndPassword(string login, string senha)
        {
            return _usuarioRepository.GetUsuarioByUserAndPassword(login, senha);
        }
    }
}
