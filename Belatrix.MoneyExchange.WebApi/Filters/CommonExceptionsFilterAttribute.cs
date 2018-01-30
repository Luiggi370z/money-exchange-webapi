using System;
using System.Data.Entity.Validation;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Belatrix.MoneyExchange.Data;
using Belatrix.MoneyExchange.Model.Exceptions;

namespace Belatrix.MoneyExchange.WebApi.Filters
{
    public class CommonExceptionsFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception;

            switch (exception)
            {
                case DbEntityValidationException validationException:
                    actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent(validationException.ToMessage()),
                    };
                    break;
                case AggregateException aggregateException when aggregateException.InnerExceptions.Count == 1:
                    actionExecutedContext.Exception = aggregateException.InnerExceptions[0];
                    break;
                case RateCurrencyNotFoundException rateCurrencyNotFoundException:
                    actionExecutedContext.Response =
                        actionExecutedContext.Request.CreateResponse(HttpStatusCode.NotFound,
                            exception.Message.ToRequestErrorModel(nameof(RateCurrencyNotFoundException)));
                    break;
            }
        }
    }
}