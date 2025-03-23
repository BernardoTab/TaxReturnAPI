using Tax.Entities.TaxReturns;
using Tax.Services.Implementations.TaxReturns.EntityValidators;
using Tax.Services.Implementations.UnitTests.Common;

namespace Tax.Services.Implementations.UnitTests.TaxReturns.EntityValidators
{
    [TestClass]
    public class TaxReturnInfoValidatorTests :
        ValueObjectValidatorTests<
            TaxReturnInfoValidator,
            TaxReturnInfo>
    {
        protected override TaxReturnInfoValidator CreateValidator()
        {
            return new TaxReturnInfoValidator();
        }

        protected override TaxReturnInfo CreateValidEntity()
        {
            return new TaxReturnInfo
            {
                GrossValue = 50,
                AustrianVATRate = VATRate.VAT13Percent
            };
        }

        [TestMethod]
        public async Task ValidateAsync_GrossAndNetValueAreSet_IncorrectNumberOfValidAmountValuesExceptionIsThrown()
        {
            ValueObject.NetValue = 50;

            Func<Task> testAction = async () => await ValueObjectValidator.ValidateAsync(ValueObject);

            await Assert.That.ThrowsIncorrectNumberOfValidAmountValuesExceptionAsync(testAction, propertyCount: 2);
        }

        [TestMethod]
        public async Task ValidateAsync_GrossAndVATValueAreSet_IncorrectNumberOfValidAmountValuesExceptionIsThrown()
        {
            ValueObject.VATValue = 50;

            Func<Task> testAction = async () => await ValueObjectValidator.ValidateAsync(ValueObject);

            await Assert.That.ThrowsIncorrectNumberOfValidAmountValuesExceptionAsync(testAction, propertyCount: 2);
        }

        [TestMethod]
        public async Task ValidateAsync_NetAndVATValueAreSet_IncorrectNumberOfValidAmountValuesExceptionIsThrown()
        {
            ValueObject.GrossValue = default;
            ValueObject.NetValue = 50;
            ValueObject.VATValue = 50;

            Func<Task> testAction = async () => await ValueObjectValidator.ValidateAsync(ValueObject);

            await Assert.That.ThrowsIncorrectNumberOfValidAmountValuesExceptionAsync(testAction, propertyCount: 2);
        }

        [TestMethod]
        public async Task ValidateAsync_AllAmountValuesAreSet_IncorrectNumberOfValidAmountValuesExceptionIsThrown()
        {
            ValueObject.GrossValue = 60;
            ValueObject.NetValue = 50;
            ValueObject.VATValue = 40;

            Func<Task> testAction = async () => await ValueObjectValidator.ValidateAsync(ValueObject);

            await Assert.That.ThrowsIncorrectNumberOfValidAmountValuesExceptionAsync(testAction, propertyCount: 3);
        }

        [TestMethod]
        public async Task ValidateAsync_NoAmountValueIsSet_IncorrectNumberOfValidAmountValuesExceptionIsThrown()
        {
            ValueObject.GrossValue = default;
            ValueObject.NetValue = default;
            ValueObject.VATValue = default;

            Func<Task> testAction = async () => await ValueObjectValidator.ValidateAsync(ValueObject);

            await Assert.That.ThrowsIncorrectNumberOfValidAmountValuesExceptionAsync(testAction, propertyCount: 0);
        }

        [TestMethod]
        [DataRow(0.0)]
        [DataRow(-50.0)]
        public async Task ValidateAsync_GrossValueIsSet_ValueIsInvalid_ValueNotSupportedExceptionIsThrown(double value)
        {
            ValueObject.GrossValue = (decimal)value;

            Func<Task> testAction = async () => await ValueObjectValidator.ValidateAsync(ValueObject);

            await Assert.That.ThrowsValueNotSupportedExceptionAsync(
                testAction,
                ValueObject.GrossValue,
                nameof(TaxReturnInfo.GrossValue));
        }

        [TestMethod]
        [DataRow(0.0)]
        [DataRow(-50.0)]
        public async Task ValidateAsync_NetValueIsSet_ValueIsInvalid_ValueNotSupportedExceptionIsThrown(double value)
        {
            ValueObject.GrossValue = default;
            ValueObject.NetValue = (decimal)value;

            Func<Task> testAction = async () => await ValueObjectValidator.ValidateAsync(ValueObject);

            await Assert.That.ThrowsValueNotSupportedExceptionAsync(
                testAction,
                ValueObject.NetValue,
                nameof(TaxReturnInfo.NetValue));
        }

        [TestMethod]
        [DataRow(0.0)]
        [DataRow(-50.0)]
        public async Task ValidateAsync_VatValueIsSet_ValueIsInvalid_ValueNotSupportedExceptionIsThrown(double value)
        {
            ValueObject.GrossValue = default;
            ValueObject.VATValue = (decimal)value;

            Func<Task> testAction = async () => await ValueObjectValidator.ValidateAsync(ValueObject);

            await Assert.That.ThrowsValueNotSupportedExceptionAsync(
                testAction,
                ValueObject.VATValue,
                nameof(TaxReturnInfo.VATValue));
        }

        [TestMethod]
        public async Task ValidateAsync_AustrianVATRateIsDefault_InvalidVATRateValueExceptionIsThrown()
        {
            ValueObject.AustrianVATRate = default;

            Func<Task> testAction = async () => await ValueObjectValidator.ValidateAsync(ValueObject);

            await Assert.That.ThrowsInvalidVATRateValueExceptionAsync(testAction);
        }
    }
}
