using GoFast.API.Models;

namespace GoFast.API.Interfaces.Repositories
{
    public interface IBlobStorageRepository : IBaseRepository<BlobStorage> 
    {
        Task<List<BlobStorage>> GetByIdUsuario(string idUsuario);
    }
}
