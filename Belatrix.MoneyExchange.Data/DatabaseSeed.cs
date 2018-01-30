using System;
using System.Collections.Generic;
using System.Data.Entity;
using Belatrix.MoneyExchange.Model;

namespace Belatrix.MoneyExchange.Data
{
    public class DatabaseSeed : CreateDatabaseIfNotExists<MoneyExchangeContext>
    {
        protected override void Seed(MoneyExchangeContext context)
        {
            var rates = new List<Rate>
            {
                new Rate { CurrencyFrom = "USD", CurrencyTo = "AUD", DateTime = DateTime.Now, Value = 1.2367 },
                new Rate { CurrencyFrom = "USD", CurrencyTo = "BGN", DateTime = DateTime.Now, Value = 1.5727 },
                new Rate { CurrencyFrom = "USD", CurrencyTo = "BRL", DateTime = DateTime.Now, Value = 3.148 },
                new Rate { CurrencyFrom = "USD", CurrencyTo = "CAD", DateTime = DateTime.Now, Value = 1.2323 },
                new Rate { CurrencyFrom = "USD", CurrencyTo = "CHF", DateTime = DateTime.Now, Value = 0.9353 },
                new Rate { CurrencyFrom = "USD", CurrencyTo = "CNY", DateTime = DateTime.Now, Value = 6.3278 },
                new Rate { CurrencyFrom = "USD", CurrencyTo = "CZK", DateTime = DateTime.Now, Value = 20.39 },
                new Rate { CurrencyFrom = "USD", CurrencyTo = "DKK", DateTime = DateTime.Now, Value = 5.9849 },
                new Rate { CurrencyFrom = "USD", CurrencyTo = "EUR", DateTime = DateTime.Now, Value = 0.8041 },
                new Rate { CurrencyFrom = "USD", CurrencyTo = "GBP", DateTime = DateTime.Now, Value = 0.7022 },
                new Rate { CurrencyFrom = "USD", CurrencyTo = "HKD", DateTime = DateTime.Now, Value = 7.8178 },
                new Rate { CurrencyFrom = "USD", CurrencyTo = "HRK", DateTime = DateTime.Now, Value = 5.9657 },
                new Rate { CurrencyFrom = "USD", CurrencyTo = "HUF", DateTime = DateTime.Now, Value = 249.12 },
                new Rate { CurrencyFrom = "USD", CurrencyTo = "IDR", DateTime = DateTime.Now, Value = 13315 },
                new Rate { CurrencyFrom = "USD", CurrencyTo = "ILS", DateTime = DateTime.Now, Value = 3.3894 },
                new Rate { CurrencyFrom = "USD", CurrencyTo = "INR", DateTime = DateTime.Now, Value = 63.61 },
                new Rate { CurrencyFrom = "USD", CurrencyTo = "JPY", DateTime = DateTime.Now, Value = 109.32 },
                new Rate { CurrencyFrom = "USD", CurrencyTo = "KRW", DateTime = DateTime.Now, Value = 1064.4 },
                new Rate { CurrencyFrom = "USD", CurrencyTo = "MXN", DateTime = DateTime.Now, Value = 18.549 },
                new Rate { CurrencyFrom = "USD", CurrencyTo = "MYR", DateTime = DateTime.Now, Value = 3.8742 },
                new Rate { CurrencyFrom = "USD", CurrencyTo = "NOK", DateTime = DateTime.Now, Value = 7.6918 },
                new Rate { CurrencyFrom = "USD", CurrencyTo = "NZD", DateTime = DateTime.Now, Value = 1.3624 },
                new Rate { CurrencyFrom = "USD", CurrencyTo = "PHP", DateTime = DateTime.Now, Value = 51.055 },
                new Rate { CurrencyFrom = "USD", CurrencyTo = "PLN", DateTime = DateTime.Now, Value = 3.3308 },
                new Rate { CurrencyFrom = "USD", CurrencyTo = "RON", DateTime = DateTime.Now, Value = 3.7523 },
                new Rate { CurrencyFrom = "USD", CurrencyTo = "RUB", DateTime = DateTime.Now, Value = 55.906 },
                new Rate { CurrencyFrom = "USD", CurrencyTo = "SEK", DateTime = DateTime.Now, Value = 7.8807 },
                new Rate { CurrencyFrom = "USD", CurrencyTo = "SGD", DateTime = DateTime.Now, Value = 1.3074 },
                new Rate { CurrencyFrom = "USD", CurrencyTo = "THB", DateTime = DateTime.Now, Value = 31.39 },
                new Rate { CurrencyFrom = "USD", CurrencyTo = "TRY", DateTime = DateTime.Now, Value = 3.75 },
                new Rate { CurrencyFrom = "USD", CurrencyTo = "ZAR", DateTime = DateTime.Now, Value = 11.891 }
            };

            rates.ForEach(p => context.Set<Rate>().Add(p));

            context.SaveChanges();
        }
    }
}
