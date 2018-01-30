using System;

namespace Belatrix.MoneyExchange.Model
{
   public  class RatesDto
    {
        public string Base { get; set; }

        public string Date { get; set; }

        public object Rates { get; set; }
    }
}
