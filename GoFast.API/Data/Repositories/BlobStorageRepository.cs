using GoFast.API.Interfaces.Repositories;
using GoFast.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;

namespace GoFast.API.Data.Repositories
{
    public class BlobStorageRepository : BaseRepository<BlobStorage>, IBlobStorageRepository
    {
        private readonly SqlContext _sqlContext;

        public BlobStorageRepository(SqlContext sqlContext) : base(sqlContext)
        {
            _sqlContext = sqlContext;
        }
    }
}
