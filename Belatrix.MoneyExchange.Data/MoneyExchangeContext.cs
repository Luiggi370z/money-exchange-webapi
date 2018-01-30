using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Belatrix.MoneyExchange.Model;

namespace Belatrix.MoneyExchange.Data
{
    public class MoneyExchangeContext : DbContext, IUnitOfWork
    {
        public MoneyExchangeContext() 
            : base("MoneyExchange") 
        {

            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Rate> Rates { get; set; }

        IQueryable<TEntity> IUnitOfWork.Set<TEntity>() => Set<TEntity>();

        TEntity IUnitOfWork.Add<TEntity>(TEntity entity) => Add(entity);

        IEnumerable<TEntity> IUnitOfWork.AddRange<TEntity>(IEnumerable<TEntity> entities) => AddRange(entities);

        TEntity IUnitOfWork.Remove<TEntity>(TEntity entity) => Remove(entity);

        IEnumerable<TEntity> IUnitOfWork.RemoveRange<TEntity>(IEnumerable<TEntity> entities) => RemoveRange(entities);

        async Task<TEntity> IUnitOfWork.FindAsync<TEntity>(params object[] keyValues) => await Set<TEntity>().FindAsync(keyValues);

        public virtual void SetEntityState(IEntity entity) => SetEntityState(entity, entity.Id == 0 ? EntityState.Added : EntityState.Modified);

        public virtual void SetEntityState<TEntity>(TEntity entity, EntityState newState)
            where TEntity : class
            => Entry(entity).State = newState;

        public virtual IEnumerable<string> GetEntityProperties<TEntity>(TEntity entity)
            where TEntity : class
            => Entry(entity).CurrentValues.PropertyNames;

        public virtual void SetIsModified<TEntity>(TEntity entity, string property, bool value)
            where TEntity : class
            => Entry(entity).Property(property).IsModified = value;

        public virtual void SetIsModified<TEntity, TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> property, bool value)
            where TEntity : class
            => Entry(entity).Property(property).IsModified = value;

        async Task<int> IUnitOfWork.SaveChangesAsync() => await SaveChangesAsync();

        public virtual TEntity Add<TEntity>(TEntity entity)
            where TEntity : class
        {
            return Set<TEntity>().Add(entity);
        }

        public virtual IEnumerable<TEntity> AddRange<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class
        {
            return Set<TEntity>().AddRange(entities);
        }

        public virtual TEntity Remove<TEntity>(TEntity entity)
            where TEntity : class
        {
            return Set<TEntity>().Remove(entity);
        }

        public virtual IEnumerable<TEntity> RemoveRange<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class
        {
            return Set<TEntity>().RemoveRange(entities);
        }
    }
}
