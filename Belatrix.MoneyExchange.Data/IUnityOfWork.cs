using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Belatrix.MoneyExchange.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IQueryable<TEntity> Set<TEntity>()
            where TEntity : class;

        TEntity Add<TEntity>(TEntity entity)
            where TEntity : class;

        IEnumerable<TEntity> AddRange<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class;

        TEntity Remove<TEntity>(TEntity entity)
            where TEntity : class;

        IEnumerable<TEntity> RemoveRange<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class;

        Task<TEntity> FindAsync<TEntity>(params object[] keyValues)
            where TEntity : class;

        IEnumerable<string> GetEntityProperties<TEntity>(TEntity entity)
            where TEntity : class;

        void SetEntityState<TEntity>(TEntity entity, EntityState newState)
            where TEntity : class;

        void SetIsModified<TEntity>(TEntity entity, string property, bool value)
            where TEntity : class;

        void SetIsModified<TEntity, TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> property,
            bool value)
            where TEntity : class;

        Task<int> SaveChangesAsync();
    }
}
