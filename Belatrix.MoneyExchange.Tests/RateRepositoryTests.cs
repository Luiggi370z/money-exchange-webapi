using System;
using Belatrix.MoneyExchange.Data;
using Belatrix.MoneyExchange.Model;
using Belatrix.MoneyExchange.Model.Exceptions;
using Belatrix.MoneyExchange.Server;
using Belatrix.MoneyExchange.Server.Queries;
using Moq;
using Xunit;
using Newtonsoft.Json.Linq;

namespace Belatrix.MoneyExchange.Tests
{
    public class RateRepositoryTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mock<RatesQuery> _ratesQuery;

        private RateRepository RateRepository => new RateRepository(_ratesQuery.Object);

        public RateRepositoryTests()
        {
            var rate1 = new Rate { CurrencyFrom = "USD", CurrencyTo = "AUD", DateTime = DateTime.Now, Value = 1.2367 };
            var rate2 = new Rate { CurrencyFrom = "USD", CurrencyTo = "BGN", DateTime = DateTime.Now, Value = 1.5727 };
            var rate3 = new Rate { CurrencyFrom = "USD", CurrencyTo = "BRL", DateTime = DateTime.Now, Value = 3.148 } ;

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupDbSet(new[] { rate1, rate2, rate3 });

            _unitOfWork = mockUnitOfWork.Object;

            _ratesQuery = new Mock<RatesQuery>(_unitOfWork);
        }

        [Fact]
        public void Execute_GetRatesDto_ShouldGetAllCurrenciesForOneExistingCurrency()
        {
            var request = new RatesRequestQuery()
            {
                Base = "USD"
            };

            var results = RateRepository.GetRatesDto(request);
            var expectedRates = JObject.FromObject(new
            {
                AUD = 1.2367,
                BGN = 1.5727,
                BRL = 3.148
            });

            Assert.NotNull(results);
            Assert.Equal(results.Base, request.Base);
            Assert.Equal(results.Date, DateTime.Now.ToString("yyyy-MM-dd"));
            Assert.Equal(results.Rates, expectedRates);
        }

        [Fact]
        public void Execute_GetRatesDto_ShouldGetOnlyOneCurrencyRate()
        {
            var request = new RatesRequestQuery()
            {
                Base = "USD",
                Symbols = "AUD"
            };

            var results = RateRepository.GetRatesDto(request);
            var expectedRates = JObject.FromObject(new
            {
                AUD = 1.2367,
            });

            Assert.NotNull(results);
            Assert.Equal(results.Base, request.Base);
            Assert.Equal(results.Date, DateTime.Now.ToString("yyyy-MM-dd"));
            Assert.Equal(results.Rates, expectedRates);
        }

        [Fact]
        public void Execute_GetRatesDto_ShouldThrowExceptionWhenSingleCurrencyRateWasNotFound()
        {
            var request = new RatesRequestQuery()
            {
                Base = "XXX",
                Symbols = "YYY"
            };
            
            var exception = Assert.Throws(typeof(RateCurrencyNotFoundException), () =>
                RateRepository.GetRatesDto(request));
            Assert.Equal(exception.Message, $"Currency conversion rate from '{request.Base}' to '{request.Symbols}' was not found.");
        }

        [Fact]
        public void Execute_GetRatesDto_ShouldReturnEmptyObjectIfAnyCurrencyRateWereFound()
        {
            var request = new RatesRequestQuery()
            {
                Base = "XXX"
            };

            var results = RateRepository.GetRatesDto(request);
            var expectedRates = JObject.FromObject(new { });

            Assert.NotNull(results);
            Assert.Equal(results.Base, request.Base);
            Assert.Equal(results.Date, DateTime.Now.ToString("yyyy-MM-dd"));
            Assert.Equal(results.Rates, expectedRates);
        }
    }
}
