using NSubstitute;
using Tax.Entities.TaxReturns;
using Tax.Services.Implementations.Common.Validations;
using Tax.Services.Implementations.TaxReturns.Commands.Validators;
using Tax.Services.Implementations.UnitTests.Common;
using Tax.Services.TaxReturns.Commands;

namespace Tax.Services.Implementations.UnitTests.TaxReturns.Commands.Validators
{
    [TestClass]
    public class ProcessTaxReturnInfoCommandValidatorTests :
        CommandValidatorTests<
            ProcessTaxReturnInfoCommandValidator,
            ProcessTaxReturnInfoCommand,
            TaxReturnInfo>
    {
        private IValueObjectValidator<TaxReturnInfo> _taxReturnInfoValidatorMock;

        protected override ProcessTaxReturnInfoCommandValidator CreateValidator()
        {
            return new ProcessTaxReturnInfoCommandValidator(_taxReturnInfoValidatorMock);
        }

        protected override ProcessTaxReturnInfoCommand CreateValidCommand()
        {
            return new ProcessTaxReturnInfoCommand
            {
                TaxReturnInfo = new TaxReturnInfo
                {
                    GrossValue = 50,
                    AustrianVATRate = VATRate.VAT13Percent
                }
            };
        }

        protected override void InitializeDependencyMocks()
        {
            _taxReturnInfoValidatorMock = Substitute.For<IValueObjectValidator<TaxReturnInfo>>();
        }

        [TestMethod]
        public async Task ValidateAsync_TaxReturnInfoIsDefault_MissingRequiredPropertyExceptionIsThrown()
        {
            Command.TaxReturnInfo = default;

            Func<Task> testAction = async () => await CommandValidator.ValidateAsync(Command);

            await Assert.That.ThrowsMissingRequiredPropertyExceptionAsync(
                testAction,
                nameof(Command.TaxReturnInfo));
        }

        [TestMethod]
        public async Task ValidateAsync_CommandIsValid_TaxReturnInfoValidatorIsCalled()
        {
            await CommandValidator.ValidateAsync(Command);

            await _taxReturnInfoValidatorMock
                .Received(1)
                .ValidateAsync(Command.TaxReturnInfo);
        }

    }
}
