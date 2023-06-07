using GoFast.API.Interfaces.Repositories;
using GoFast.API.Models;

namespace GoFast.API.Data.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly SqlContext _sqlContext;

        public UsuarioRepository(SqlContext sqlContext) : base(sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public Usuario GetUsuarioByUserAndPassword(string userName, string password)
        {
            var users = _sqlContext.Usuario;
            return users.Where(x => x.LoginUser.ToLower() == userName && x.Senha == password).FirstOrDefault();
        }
    }
}
