using System;
using System.Collections.Generic;
using System.Linq;
using Belatrix.MoneyExchange.Data;
using Moq;

namespace Belatrix.MoneyExchange.Tests
{
    public static class UnifOfWorkExtensions
    {
        public static Mock<IUnitOfWork> SetupDbSet<TEntity>(
            this Mock<IUnitOfWork> self,
            IEnumerable<TEntity> data)
            where TEntity : class
        {
            self.SetupDbSetInternal(new DbSetMock<TEntity>(data));

            return self;
        }

        public static Mock<IUnitOfWork> SetupDbSet<TEntity>(
            this Mock<IUnitOfWork> self,
            IEnumerable<TEntity> data,
            Func<TEntity, object[], bool> find)
            where TEntity : class
        {
            self.SetupDbSetInternal(new DbSetMock<TEntity>(data, find));

            return self;
        }

        public static Mock<IUnitOfWork> SetupEmptyDbSet<TEntity>(this Mock<IUnitOfWork> self)
            where TEntity : class
            => self.SetupDbSet(Enumerable.Empty<TEntity>());

        private static void SetupDbSetInternal<TEntity>(
            this Mock<IUnitOfWork> self,
            DbSetMock<TEntity> set)
            where TEntity : class
        {
            self.Setup(c => c.Set<TEntity>()).Returns(set);
            self.Setup(x => x.FindAsync<TEntity>(It.IsAny<object[]>())).Returns<object[]>(keys => set.FindAsync(keys));
            self.Setup(x => x.Add(It.IsAny<TEntity>())).Returns<TEntity>(x => set.Add(x));
            self.Setup(x => x.AddRange(It.IsAny<IEnumerable<TEntity>>())).Returns<IEnumerable<TEntity>>(x => set.AddRange(x));
            self.Setup(x => x.Remove(It.IsAny<TEntity>())).Returns<TEntity>(x => set.Remove(x));
            self.Setup(x => x.RemoveRange(It.IsAny<IEnumerable<TEntity>>())).Returns<IEnumerable<TEntity>>(x => set.RemoveRange(x));
        }
    }
}
