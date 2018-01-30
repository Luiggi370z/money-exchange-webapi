using System;
using System.Collections.Generic;
using System.Linq;
using Belatrix.MoneyExchange.Data;
using Belatrix.MoneyExchange.Model;
using Belatrix.MoneyExchange.Server.Queries;
using Newtonsoft.Json.Linq;

namespace Belatrix.MoneyExchange.Server
{
    public class RateRepository : IRateRepository
    {
        private readonly RatesQuery _dataRateQuery;

        public RateRepository(RatesQuery dataRateQuery)
        {
            _dataRateQuery = dataRateQuery;
        }

        public void Dispose()
        {
            _dataRateQuery.Dispose();
        }

        public IEnumerable<Rate> GetAll(string currencyFrom)
        {
            return _dataRateQuery.GetAll(currencyFrom);
        }

        public Rate Get(string currencyFrom, string currencyTo)
        {
            var existingRate = _dataRateQuery.Get(currencyFrom, currencyTo);

            if (existingRate != null)
                return existingRate;

             throw new Exception($"Currency conversion from '{currencyFrom}' to '{currencyTo}' was not found.");
        }

        public RatesDto GetRatesDto(RatesRequestQuery query)
        {
            var rates = new List<Rate>();

            if (string.IsNullOrEmpty(query.Symbols))
            {
                rates.AddRange(GetAll(query.Base));
            }
            else
            {
                var existingRate = Get(query.Base, query.Symbols);

                if (existingRate != null)
                    rates.Add(existingRate);
            }

            return new RatesDto
            {
                Base = query.Base,
                Date = DateTime.Now.ToString("yyyy-MM-dd"),
                Rates = JObject.FromObject(rates.ToDictionary(r => r.CurrencyTo, r => r.Value)),
            };
        }
    }
}
