using GoFast.Domain.Core.Interfaces.Repositories;
using GoFast.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFast.Infrastructure.Data.Repositories
{
    internal class MotoristaRepository : BaseRepository<Motorista>
    {
        private readonly SqlContext _sqlContext;

        public MotoristaRepository(SqlContext sqlContext) : base(sqlContext)
        {
            _sqlContext = sqlContext;
        }
    }
}
