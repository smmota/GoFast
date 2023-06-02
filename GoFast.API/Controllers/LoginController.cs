using GoFast.API.Services;
using GoFast.Application.Dtos;
using GoFast.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoFast.API.Controllers
{
    [Route("Login")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IUsuarioApplicationService _usuarioApplicationService;

        public LoginController(IUsuarioApplicationService usuarioApplicationService)
        {
            _usuarioApplicationService = usuarioApplicationService;
        }

        [HttpPost]
        [Route("auth")]
        [AllowAnonymous]
        public ActionResult<dynamic> Autenticate([FromBody] LoginDto loginDto)
        {
            try
            {
                if (string.IsNullOrEmpty(loginDto.LoginUser) || string.IsNullOrEmpty(loginDto.Senha))
                    return NotFound((new { message = "Informe o usuário e senha!" }));

                UsuarioDto usuarioDto = new UsuarioDto()
                {
                    LoginUser = loginDto.LoginUser,
                    Senha = loginDto.Senha
                };

                var usuario = _usuarioApplicationService.GetUsuarioByUserAndPassword(usuarioDto.LoginUser, usuarioDto.Senha);

                if (usuario == null)
                    return NotFound((new { message = "Usuário ou senha inválidos" }));

                var token = TokenService.GenerateToken(usuario);
                usuario.Senha = "";

                return new
                {
                    token = token
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
