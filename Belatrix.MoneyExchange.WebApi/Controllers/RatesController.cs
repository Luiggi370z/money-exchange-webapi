using System;
using System.Web.Http;
using Belatrix.MoneyExchange.Model;
using Belatrix.MoneyExchange.Server;

namespace Belatrix.MoneyExchange.WebApi.Controllers
{
    [RoutePrefix("api/rates")]
    public class RatesController : ApiController
    {
        private readonly IRateRepository _ratetRepository;

        public RatesController(IRateRepository rateRepository)
        {
            _ratetRepository = rateRepository;
        }

        [HttpGet]
        [Route("latest")]
        public object GetRates([FromUri] RatesRequestQuery query)
        {
            if (string.IsNullOrEmpty(query.Base))
                throw new ArgumentNullException($"Parameter {query.Base} cannot be null", nameof(query.Base));

            return _ratetRepository.GetRatesDto(query);
        }
    }
}