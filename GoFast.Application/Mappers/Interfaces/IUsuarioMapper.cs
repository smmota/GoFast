using GoFast.Application.Dtos;
using GoFast.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFast.Application.Mappers.Interfaces
{
    public interface IUsuarioMapper
    {
        Usuario DtoToEntityMapper(UsuarioDto usuarioDto);

        IEnumerable<UsuarioDto> ListUsuariosDtoMapper(IEnumerable<Usuario> usuarios);

        UsuarioDto EntityToDtoMapper(Usuario usuario);
    }
}
