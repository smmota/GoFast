using GoFast.API.Models;

namespace GoFast.API.Interfaces.Repositories
{
    public interface IBlobStorageRepository : IBaseRepository<BlobStorage> 
    {
        public List<BlobStorage> GetByIdUsuario(string idUsuario);
    }
}
