using GoFast.Application.Dtos;
using GoFast.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFast.Application.Mappers
{
    public class UsuarioMapper
    {
        public Usuario DtoToEntityMapper(UsuarioDto usuarioDto)
        {
            return
                new Usuario()
                {
                    Id = usuarioDto.Id,
                    Nome = usuarioDto.Nome,
                    LoginUser = usuarioDto.LoginUser,
                    Senha = usuarioDto.Senha,
                    Ativo = usuarioDto.Ativo,
                };
        }

        public UsuarioDto EntityToDtoMapper(Usuario usuario)
        {
            return
                new UsuarioDto()
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    LoginUser = usuario.LoginUser,
                    Senha = usuario.Senha,
                    Ativo = usuario.Ativo,
                };
        }

        public IEnumerable<UsuarioDto> ListUsuariosDtoMapper(IEnumerable<Usuario> usuarios)
        {
            return
                usuarios.Select(x => new UsuarioDto
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    LoginUser = x.LoginUser,
                    Senha = x.Senha,
                    Ativo = x.Ativo,
                });
        }
    }
}
