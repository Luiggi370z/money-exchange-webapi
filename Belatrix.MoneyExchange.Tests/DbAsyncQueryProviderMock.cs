using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Belatrix.MoneyExchange.Tests
{
    internal class DbAsyncQueryProviderMock<TEntity> : IDbAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        internal DbAsyncQueryProviderMock(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression) => new DbAsyncEnumerableMock<TEntity>(expression);

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression) => new DbAsyncEnumerableMock<TElement>(expression);

        public object Execute(Expression expression) => _inner.Execute(expression);

        public TResult Execute<TResult>(Expression expression) => _inner.Execute<TResult>(expression);

        public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken) => Task.FromResult(Execute(expression));

        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken) => Task.FromResult(Execute<TResult>(expression));
    }
}
