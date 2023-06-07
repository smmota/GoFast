using GoFast.API.Interfaces.Repositories;
using GoFast.API.Models;

namespace GoFast.API.Data.Repositories
{
    internal class MotoristaRepository : BaseRepository<Motorista>, IMotoristaRepository
    {
        private readonly SqlContext _sqlContext;

        public MotoristaRepository(SqlContext sqlContext) : base(sqlContext)
        {
            _sqlContext = sqlContext;
        }
    }
}
