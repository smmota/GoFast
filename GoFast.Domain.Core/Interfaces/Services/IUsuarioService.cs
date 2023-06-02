using GoFast.Domain.Entities;

namespace GoFast.Domain.Core.Interfaces.Services
{
    public interface IUsuarioService : IBaseService<Usuario>
    {
        Usuario GetUsuarioByUserAndPassword(string login, string senha);
    }
}
