using GoFast.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFast.Application.Interfaces
{
    public interface IUsuarioApplicationService
    {
        bool Add(UsuarioDto usuarioDto);

        bool Update(UsuarioDto usuarioDto);

        bool Remove(int id);

        IEnumerable<UsuarioDto> GetAll();

        UsuarioDto GetById(int id);

        IEnumerable<UsuarioDto> GetAllAtivos();

        UsuarioDto GetUsuarioByUserAndPassword(string login, string senha);
    }
}
