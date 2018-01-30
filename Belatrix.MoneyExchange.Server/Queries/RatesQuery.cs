using System;
using System.Collections.Generic;
using System.Linq;
using Belatrix.MoneyExchange.Data;
using Belatrix.MoneyExchange.Model;

namespace Belatrix.MoneyExchange.Server.Queries
{
    public class RatesQuery : IDisposable
    {
        private readonly IUnitOfWork _context;

        public RatesQuery(IUnitOfWork context)
        {
            _context = context;
        }

        public IEnumerable<Rate> GetAll(string currencyFrom)
        {
            return _context.Set<Rate>()
                .Where(r => r.CurrencyFrom == currencyFrom);
        }

        public Rate Get(string currencyFrom, string currencyTo)
        {
            return _context.Set<Rate>()
                .FirstOrDefault(r => r.CurrencyFrom == currencyFrom && r.CurrencyTo == currencyTo);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
