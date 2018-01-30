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

            var validationException = exception as DbEntityValidationException;
            if (validationException != null)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(validationException.ToMessage()),
                };
            }

            var aggregateException = exception as AggregateException;
            if (aggregateException != null && aggregateException.InnerExceptions.Count == 1)
            {
                actionExecutedContext.Exception = aggregateException.InnerExceptions[0];
            }

            var rateCurrencyNotFoundException = exception as RateCurrencyNotFoundException;
            if (rateCurrencyNotFoundException != null)
            {
                actionExecutedContext.Response =
                    actionExecutedContext.Request.CreateResponse(HttpStatusCode.NotFound,
                        exception.Message.ToRequestErrorModel(nameof(RateCurrencyNotFoundException)));
            }
        }
    }
}