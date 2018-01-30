using System;
using System.Collections.Generic;
using Belatrix.MoneyExchange.Model;

namespace Belatrix.MoneyExchange.Server
{
    public interface IRateRepository : IDisposable
    {
        IEnumerable<Rate> GetAll(string currencyFrom);

        Rate Get(string currencyFrom, string currencyTo);

        RatesDto GetRatesDto(RatesRequestQuery query);
    }
}