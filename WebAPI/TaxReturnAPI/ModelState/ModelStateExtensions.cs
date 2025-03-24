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
            if (modelState.Any(mse => mse.Key.ToLower().Contains(nameof(TaxReturnInfo.AustrianVATRate).ToLower())))
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
            if (modelState.Any(mse => mse.Key.ToLower().Contains(nameof(TaxReturnInfo.NetValue).ToLower())))
            {
                propName = nameof(TaxReturnInfo.NetValue);
            }
            else if (modelState.Any(mse => mse.Key.ToLower().Contains(nameof(TaxReturnInfo.GrossValue).ToLower())))
            {
                propName = nameof(TaxReturnInfo.GrossValue);
            }
            else if (modelState.Any(mse => mse.Key.ToLower().Contains(nameof(TaxReturnInfo.VATValue).ToLower())))
            {
                propName = nameof(TaxReturnInfo.VATValue);
            }
            return propName;
        }
    }
}
