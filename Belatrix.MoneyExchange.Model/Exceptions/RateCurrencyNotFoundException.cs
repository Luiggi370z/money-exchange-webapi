using System;

namespace Belatrix.MoneyExchange.Model.Exceptions
{
    [Serializable]
    public class RateCurrencyNotFoundException : Exception
    {
        public string CurrencyFrom { get; set; }

        public string CurrencyTo { get; set; }

        public RateCurrencyNotFoundException(string currencyFrom, string currencyTo)
            : base(BuildMessage(currencyFrom, currencyTo))
        {
            CurrencyFrom = currencyFrom;
            CurrencyTo = currencyTo;
        }

        private static string BuildMessage(string currencyFrom, string currencyTo)
        {
            // TODO: A new message can be created when there are not results for a specific Base currency, but it has more sense to return an empty object of rates in that case.
            return $"Currency conversion rate from '{currencyFrom}' to '{currencyTo}' was not found.";
        }
    }
}
