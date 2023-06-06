using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFast.API.Interfaces.Repositories
{ 
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Add(TEntity obj);

        void Update(TEntity obj);

        void Remove(Guid id);

        IEnumerable<TEntity> GetAll();

        TEntity GetById(Guid id);
    }
}
