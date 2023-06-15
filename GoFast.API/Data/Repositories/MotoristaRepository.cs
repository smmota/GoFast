using AutoMapper;
using GoFast.API.Interfaces.Repositories;
using GoFast.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GoFast.API.Data.Repositories
{
    internal class MotoristaRepository : BaseRepository<Motorista>, IMotoristaRepository
    {
        private readonly SqlContext _sqlContext;

        public MotoristaRepository(SqlContext sqlContext) : base(sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public async Task<Motorista> GetMotoristaById(Guid id)
        {
            return await _sqlContext.Motorista.Where(x => x.Id == id).Include(y => y.Carro).Include(y => y.Carro.DocumentoCarro.BlobStorage).Include(y => y.Carro.DocumentoCarro).Include(y => y.Endereco).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Motorista>> GetAllMotoristas()
        {
            return await _sqlContext.Motorista.Include(x => x.Carro).Include(x => x.Carro.DocumentoCarro).Include(x => x.Carro.DocumentoCarro.BlobStorage).Include(x => x.Endereco).ToListAsync();
        }
    }
}
