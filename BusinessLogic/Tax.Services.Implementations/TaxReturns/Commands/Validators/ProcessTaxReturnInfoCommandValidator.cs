using Tax.Entities.Exceptions;
using Tax.Entities.TaxReturns;
using Tax.Services.Implementations.Common.Validations;
using Tax.Services.TaxReturns.Commands;

namespace Tax.Services.Implementations.TaxReturns.Commands.Validators
{
    public class ProcessTaxReturnInfoCommandValidator :
        ICommandValidator<ProcessTaxReturnInfoCommand, ProcessedTaxReturnInfo>
    {
        private readonly IValueObjectValidator<TaxReturnInfo> _taxReturnInfoValidator;
        private ProcessTaxReturnInfoCommand _command;

        public ProcessTaxReturnInfoCommandValidator(IValueObjectValidator<TaxReturnInfo> taxReturnInfoValidator)
        {
            _taxReturnInfoValidator = taxReturnInfoValidator;
        }

        public async Task ValidateAsync(ProcessTaxReturnInfoCommand command)
        {
            _command = command ?? throw new ArgumentNullException(nameof(command));
            await ValidatePropertiesAsync();
        }

        private async Task ValidatePropertiesAsync()
        {
            await ValidateTaxReturnInfoAsync();
        }

        private async Task ValidateTaxReturnInfoAsync()
        {
            if (_command.TaxReturnInfo == default)
            {
                throw new MissingRequiredPropertyException(
                    nameof(TaxReturnInfo));
            }
            await _taxReturnInfoValidator.ValidateAsync(_command.TaxReturnInfo);
        }
    }
}
