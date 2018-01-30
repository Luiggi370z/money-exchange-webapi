using System;

namespace Belatrix.MoneyExchange.Model
{
    public class Rate : IEntity
    {
        public int Id { get; set; }

        public string CurrencyFrom { get; set; }

        public string CurrencyTo{ get; set; }

        public double Value { get; set; }

        public DateTime DateTime { get; set; }
    }
}
