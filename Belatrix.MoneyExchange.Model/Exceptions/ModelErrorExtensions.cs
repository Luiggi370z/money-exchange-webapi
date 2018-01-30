namespace Belatrix.MoneyExchange.Model.Exceptions
{
    public static class ModelErrorExtensions
    {
        public static ResponseErrorModel ToRequestErrorModel(this string message, string errorName)
        {
            return new ResponseErrorModel
            {
                Error = errorName,
                Description = message,
            };
        }
    }
}
