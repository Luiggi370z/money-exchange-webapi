using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Belatrix.MoneyExchange.Model;

namespace Belatrix.MoneyExchange.Tests
{
    public sealed class DbSetMock<TEntity> : DbSet<TEntity>, IQueryable, IEnumerable<TEntity>
        where TEntity : class
    {
        private readonly ObservableCollection<TEntity> _data;
        private readonly IQueryable _query;
        private readonly bool _isIEntity;
        private readonly Func<TEntity, object[], bool> _find;

        public DbSetMock()
        {
            _data = new ObservableCollection<TEntity>();
            _query = _data.AsQueryable();
            _isIEntity = typeof(IEntity).IsAssignableFrom(typeof(TEntity));
        }

        public DbSetMock(IEnumerable<TEntity> data)
            : this()
        {
            AddRange(data);
        }

        public DbSetMock(IEnumerable<TEntity> data, Func<TEntity, object[], bool> find)
            : this()
        {
            AddRange(data);
            _find = find;
        }

        public override TEntity Add(TEntity item)
        {
            _data.Add(item);

            if (_isIEntity && ((IEntity)item).Id == 0)
                ((IEntity)item).Id = _data.Cast<IEntity>().Max(x => x.Id) + 1;

            return item;
        }

        public override IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                Add(entity);
            return entities;
        }

        public override TEntity Remove(TEntity item)
        {
            _data.Remove(item);
            return item;
        }

        public override IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities.ToList())
                Remove(entity);
            return entities;
        }

        public override TEntity Attach(TEntity item)
        {
            if (!_data.Contains(item))
                _data.Add(item);
            return item;
        }

        public override TEntity Create() => Activator.CreateInstance<TEntity>();

        public override TDerivedEntity Create<TDerivedEntity>() => Activator.CreateInstance<TDerivedEntity>();

        public override ObservableCollection<TEntity> Local => _data;

        public override TEntity Find(params object[] keyValues)
        {
            if (!_isIEntity)
                if (_find == null)
                    throw new NotImplementedException();
                else
                    return _data.SingleOrDefault(x => _find(x, keyValues));

            return (TEntity)_data.Cast<IEntity>()
                .SingleOrDefault(x => x.Id == (int)keyValues[0]);
        }

        public override Task<TEntity> FindAsync(params object[] keyValues) => Task.FromResult(Find(keyValues));

        public override DbQuery<TEntity> AsNoTracking() => this;

        Type IQueryable.ElementType => _query.ElementType;

        IEnumerator IEnumerable.GetEnumerator() => _data.GetEnumerator();

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator() => _data.GetEnumerator();

        IQueryProvider IQueryable.Provider => new DbAsyncQueryProviderMock<TEntity>(_query.Provider);

        Expression IQueryable.Expression => _query.Expression;
    }
}
