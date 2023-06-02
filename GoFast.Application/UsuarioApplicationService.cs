using GoFast.Application.Dtos;
using GoFast.Application.Interfaces;
using GoFast.Application.Mappers.Interfaces;
using GoFast.Domain.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFast.Application
{
    public class UsuarioApplicationService : IUsuarioApplicationService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IUsuarioMapper _usuarioMapper;

        public UsuarioApplicationService(IUsuarioService usuarioService, IUsuarioMapper usuarioMapper)
        {
            _usuarioService = usuarioService;
            _usuarioMapper = usuarioMapper;
        }

        public bool Add(UsuarioDto usuarioDto)
        {
            bool retorno = false;

            try
            {
                if (usuarioDto.Id != 0)
                    throw new System.Exception("Usuário não cadastrado! O Id deve ser 0");

                var usuario = _usuarioMapper.DtoToEntityMapper(usuarioDto);
                _usuarioService.Add(usuario);

                retorno = true;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Erro ao cadastrar o usuário. " + ex.Message);
            }

            return retorno;
        }

        public IEnumerable<UsuarioDto> GetAll()
        {
            var usuarios = _usuarioService.GetAll();
            return _usuarioMapper.ListUsuariosDtoMapper(usuarios);
        }

        public IEnumerable<UsuarioDto> GetAllAtivos()
        {
            var usuarios = _usuarioService.GetAll().Where(x => x.Ativo == true);
            return _usuarioMapper.ListUsuariosDtoMapper(usuarios);
        }

        public UsuarioDto GetById(int id)
        {
            try
            {
                if (id == 0)
                    throw new System.Exception("O Id informado é inválido!");

                var usuario = _usuarioService.GetById(id);

                if (usuario != null)
                    return _usuarioMapper.EntityToDtoMapper(usuario);
                else
                    return null;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Erro ao obter o usuário. " + ex.Message);
            }
        }

        public UsuarioDto GetUsuarioByUserAndPassword(string login, string senha)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(senha))
                throw new System.Exception("Usuário e senha são obrigatórios!");

            var usuario = _usuarioService.GetUsuarioByUserAndPassword(login, senha);
            return usuario != null ? _usuarioMapper.EntityToDtoMapper(usuario) : null;
        }

        public bool Remove(int id)
        {
            bool retorno = false;

            try
            {
                if (id == 0)
                    throw new System.Exception("O Id informado é inválido!");

                _usuarioService.Remove(id);
                retorno = true;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Erro ao remover o usuário informado! " + ex.Message);
            }

            return retorno;
        }

        public bool Update(UsuarioDto usuarioDto)
        {
            bool retorno = false;

            try
            {
                if (usuarioDto.Id == 0)
                    throw new System.Exception("O Id informado é inválido!");

                var usuario = _usuarioMapper.DtoToEntityMapper(usuarioDto);
                _usuarioService.Update(usuario);

                retorno = true;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Erro ao atualizar o usuário! " + ex.Message);
            }

            return retorno;
        }
    }
}
