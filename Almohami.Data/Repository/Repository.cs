using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Almohami.Data;
using Almohami.Data.AlmohamiModel;

namespace Almohami.Data.Repository
{
    public class Repository<T> : IRepository<T>where T : class
    {
        protected AlmohamiContext _context;
        private DbSet<T> _dbSet;

        public Repository(AlmohamiContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        #region IRepository<T> Members

        public T GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        public IEnumerable<T> Query(Expression<Func<T, bool>> filter)
        {
            return _dbSet.Where(filter);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        /// <summary>
        /// Generic update method for the entities
        /// </summary>
        /// <param name="entityToUpdate">entityToUpdate</param>
        public void Update(T entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
            _context.Entry(entity).State = EntityState.Deleted;
        }

        /// <summary>
        /// generic method to fetch get the required table
        /// </summary>
        /// <returns>TEntity</returns>
        public IQueryable<T> Table()
        {
            return _dbSet;
        }

        #endregion
    }
}
