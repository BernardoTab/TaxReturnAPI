using Microsoft.AspNetCore.Mvc.ModelBinding;
using Tax.Entities.Exceptions;
using Tax.Entities.TaxReturns;

namespace Tax.DataTransferring.ModelBinding
{
    public class VATRateEnumModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }
            string value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;
            if (string.IsNullOrWhiteSpace(value))
            {
                bindingContext.Result = ModelBindingResult.Success(VATRate.Unknown);
                return Task.CompletedTask;
            }
            if (!Enum.TryParse(typeof(VATRate), value, true, out object? parsedEnum) || !Enum.IsDefined(typeof(VATRate), parsedEnum))
            {
                throw new InvalidVATRateValueException();
            }
            bindingContext.Result = ModelBindingResult.Success((VATRate)parsedEnum);
            return Task.CompletedTask;
        }
    }
}
