using GoFast.API.Interfaces.Repositories;
using GoFast.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GoFast.API.Data.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly SqlContext _sqlContext;

        public UsuarioRepository(SqlContext sqlContext) : base(sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public async Task<Usuario> GetUsuarioByLogin(string userName)
        {
            var users = _sqlContext.Usuario;
            return await users.Where(x => x.LoginUser.ToLower() == userName.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<Usuario> GetUsuarioByUserAndPassword(string userName, string password)
        {
            //var users = _sqlContext.Usuario;
            //return await users.Where(x => x.LoginUser.ToLower() == userName.ToLower() && x.Senha == password).FirstOrDefaultAsync();

            return await _sqlContext.Usuario.Where(x => x.LoginUser.ToLower() == userName.ToLower() && x.Senha == password).FirstOrDefaultAsync();
        }

        public async Task<bool> VerificaSeUsuarioExiste(string userName)
        {
            var users = _sqlContext.Usuario;
            var user = await users.Where(x => x.LoginUser.ToLower() == userName.ToLower()).FirstOrDefaultAsync();

            return user != null ? true : false;
        }
    }
}
