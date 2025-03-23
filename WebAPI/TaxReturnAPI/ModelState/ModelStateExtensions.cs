using Microsoft.AspNetCore.Mvc.ModelBinding;
using Tax.Entities.Exceptions;
using Tax.Entities.TaxReturns;

namespace TaxReturnAPI.ModelState
{
    public static class ModelStateExtensions
    {
        public static void ThrowInvalidModelStateExceptions(this ModelStateDictionary modelState)
        {
            string invalidDecimalPropertyName = GetInvalidDecimalPropertyName(modelState);
            if (modelState.Any(mse => mse.Key.Contains(nameof(TaxReturnInfo.AustrianVATRate))))
            {
                throw new InvalidVATRateValueException();
            }
            else if (invalidDecimalPropertyName != "")
            {
                throw new InvalidInputTypeException(invalidDecimalPropertyName);
            }
        }

        private static string GetInvalidDecimalPropertyName(ModelStateDictionary modelState)
        {
            string propName = "";
            if (modelState.Any(mse => mse.Key.Contains(nameof(TaxReturnInfo.NetValue))))
            {
                propName = nameof(TaxReturnInfo.NetValue);
            }
            else if (modelState.Any(mse => mse.Key.Contains(nameof(TaxReturnInfo.GrossValue))))
            {
                propName = nameof(TaxReturnInfo.GrossValue);
            }
            else if (modelState.Any(mse => mse.Key.Contains(nameof(TaxReturnInfo.VATValue))))
            {
                propName = nameof(TaxReturnInfo.VATValue);
            }
            return propName;
        }
    }
}
