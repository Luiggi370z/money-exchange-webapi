using System;
using System.Data.Entity.Validation;
using System.Text;

namespace Belatrix.MoneyExchange.Data
{
    public static class DbEntityValidationExceptionExtensions
    {
        public static string ToMessage(
            this DbEntityValidationException dbEntityValidationException)
        {
            var errorMessage = new StringBuilder();
            errorMessage.AppendLine(dbEntityValidationException.Message);
            errorMessage.AppendLine("Data Validation Errors: ");

            foreach (var entry in dbEntityValidationException.EntityValidationErrors)
            {
                errorMessage.AppendFormat("Entity: {0}{1}", entry.Entry.Entity.GetType().FullName, Environment.NewLine);
                foreach (var error in entry.ValidationErrors)
                    errorMessage.AppendFormat("\t{0} - {1}{2}", error.PropertyName, error.ErrorMessage, Environment.NewLine);
            }

            return errorMessage.ToString();
        }
    }
}
