using GoFast.API.Models;

namespace GoFast.API.Data.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>
    {
        private readonly SqlContext _sqlContext;

        public UsuarioRepository(SqlContext sqlContext) : base(sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public Usuario GetUsuarioByUserAndPassword(string userName, string password)
        {
            return _sqlContext.Usuario.Where(x => x.LoginUser.ToLower() == userName && x.Senha == password).FirstOrDefault();
        }
    }
}
