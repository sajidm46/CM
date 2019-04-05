
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using CMDataAccess.Repositories.Abstract;

namespace CMDataAccess.Repositories.Concrete
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected IDbTransaction Transaction { get; private set; }
        protected IDbConnection Connection { get { return Transaction.Connection; } }

        public GenericRepository(IDbTransaction transaction)
        {
            Transaction = transaction;
        }

        public void Add(TEntity entity)
        {
            using (var conn = Connection)
            {
                conn.Insert(entity, Transaction);
                Transaction.Commit();
            }
        }

        public void Delete(int Id)
        {
            using (var conn = Connection)
            {
                var entityToRemove = conn.Get<TEntity>(Id, Transaction);
                conn.Delete(entityToRemove, Transaction);
                Transaction.Commit();
            }
        }

        public void Delete(TEntity entity, IDbConnection Connection)
        {
            Connection.Delete(entity, Transaction);
        }

        public void Update(TEntity entity)
        {
            using (var conn = Connection)
            {
                conn.Update(entity, Transaction);
                Transaction.Commit();
            }
        }

        public TEntity Get(int Id)
        {
            using (var conn = Connection)
            {
                return conn.Get<TEntity>(Id, Transaction);
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            using (var conn = Connection)
            {
                return conn.GetAll<TEntity>(Transaction);
            }
        }
    }
}
