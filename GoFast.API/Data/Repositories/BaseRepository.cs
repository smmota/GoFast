using GoFast.API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoFast.API.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly SqlContext _sqlContext;

        public BaseRepository(SqlContext sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public void Add(TEntity obj)
        {
            try
            {
                _sqlContext.Set<TEntity>().Add(obj);
                _sqlContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _sqlContext.Set<TEntity>().ToList();
        }

        public TEntity GetById(Guid id)
        {
            return _sqlContext.Set<TEntity>().Find(id);
        }

        public void Remove(Guid id)
        {
            try
            {
                var obj = GetById(id);

                if (obj != null)
                {
                    _sqlContext.Set<TEntity>().Remove(obj);
                    _sqlContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(TEntity obj)
        {
            try
            {
                _sqlContext.Entry(obj).State = EntityState.Modified;
                _sqlContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal object getBlobByID(Guid idBlob)
        {
            throw new NotImplementedException();
        }
    }
}
