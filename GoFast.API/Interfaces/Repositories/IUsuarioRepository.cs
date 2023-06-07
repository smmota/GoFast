using GoFast.API.Models;

namespace GoFast.API.Interfaces.Repositories
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        public Usuario GetUsuarioByUserAndPassword(string userName, string password);

        public Usuario GetUsuarioByLogin(string userName);
    }
}
