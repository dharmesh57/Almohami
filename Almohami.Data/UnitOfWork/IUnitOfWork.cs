using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Almohami.Data.Repository;

namespace Almohami.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Save();
        void Dispose();
        IRepository<T> Repository<T>() where T : class;
        IEnumerable<TEntity> SQLQuery<TEntity>(string sql, params object[] parameters) where TEntity : class;
    }
}
