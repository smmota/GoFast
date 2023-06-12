using GoFast.API.Models;

namespace GoFast.API.Interfaces.Repositories
{
    public interface IMotoristaRepository : IBaseRepository<Motorista>
    {
        Task<Motorista> GetMotoristaById(Guid id);
        Task<IEnumerable<Motorista>> GetAllMotoristas();
    }
}
