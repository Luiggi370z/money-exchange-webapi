using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;

namespace Belatrix.MoneyExchange.Tests
{
    internal class DbAsyncEnumerableMock<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>, IQueryable<T>
    {
        public DbAsyncEnumerableMock(IEnumerable<T> enumerable)
            : base(enumerable)
        {
        }

        public DbAsyncEnumerableMock(Expression expression)
            : base(expression)
        {
        }

        public IDbAsyncEnumerator<T> GetAsyncEnumerator() => new DbAsyncEnumeratorMock<T>(this.AsEnumerable().GetEnumerator());

        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator() => GetAsyncEnumerator();

        IQueryProvider IQueryable.Provider => new DbAsyncQueryProviderMock<T>(this);
    }
}
