using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
using Tax.Entities.Exceptions;

namespace Tax.DataTransferring.ModelBinding
{
    public class DecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;
            string propertyName = bindingContext.ModelMetadata.Name;
            if (string.IsNullOrWhiteSpace(value))
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }
            if (!decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal decimalValue))
            {
                throw new InvalidInputTypeException(propertyName);
            }
            bindingContext.Result = ModelBindingResult.Success(decimalValue);
            return Task.CompletedTask;
        }
    }
}
