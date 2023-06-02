using GoFast.Domain.Core.Interfaces.Repositories;
using GoFast.Domain.Entities;

namespace GoFast.Infrastructure.Data.Repositories
{
    internal class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly SqlContext _sqlContext;

        public UsuarioRepository(SqlContext sqlContext) : base(sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public Usuario GetUsuarioByUserAndPassword(string login, string senha)
        {
            return _sqlContext.Usuario.Where(u => u.LoginUser == login && u.Senha == senha).FirstOrDefault();
        }
    }
}
