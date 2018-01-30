using System.Net;
using System.Net.Http;
using System.Web.Http;
using Belatrix.MoneyExchange.Model;
using Belatrix.MoneyExchange.Server;
using Swashbuckle.Swagger.Annotations;

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
        [SwaggerResponse(HttpStatusCode.OK, type: typeof(RatesDto))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.NotFound, type: typeof(ResponseErrorModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public HttpResponseMessage GetRates([FromUri] RatesRequestQuery query)
        {
            if (string.IsNullOrEmpty(query.Base))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"Parameter {nameof(query.Base)} is required.");
            }

            var results = _ratetRepository.GetRatesDto(query);

            return Request.CreateResponse(HttpStatusCode.OK, results);
        }
    }
}