using Tax.Entities.Exceptions;
using Tax.Entities.TaxReturns;
using Tax.Services.Implementations.Common.Validations;

namespace Tax.Services.Implementations.TaxReturns.EntityValidators
{
    public class TaxReturnInfoValidator : IValueObjectValidator<TaxReturnInfo>
    {
        private TaxReturnInfo _taxReturnInfo;

        public async Task ValidateAsync(TaxReturnInfo taxReturnInfo)
        {
            _taxReturnInfo = taxReturnInfo;
            ValidateProperties();
        }

        private void ValidateProperties()
        {
            ValidateNumberOfAmountValues();
            ValidateGrossValueIfSet();
            ValidateNetValueIfSet();
            ValidateVATValueIfSet();
            ValidateAustrianVATRate();
        }

        private void ValidateNumberOfAmountValues()
        {
            int nonDefaultValues = new[] { _taxReturnInfo.GrossValue, _taxReturnInfo.NetValue, _taxReturnInfo.VATValue }
                .Count(p => p != default && p != 0);
            if (nonDefaultValues != 1)
            {
                throw new IncorrectNumberOfValidAmountValuesException(nonDefaultValues);
            }
        }

        private void ValidateGrossValueIfSet()
        {
            if (_taxReturnInfo.GrossValue != default && _taxReturnInfo.GrossValue <= 0)
            {
                throw new ValueNotSupportedException(
                    _taxReturnInfo.GrossValue,
                    nameof(TaxReturnInfo.GrossValue),
                    nameof(TaxReturnInfo));
            }
        }

        private void ValidateNetValueIfSet()
        {
            if (_taxReturnInfo.NetValue != default && _taxReturnInfo.NetValue <= 0)
            {
                throw new ValueNotSupportedException(
                    _taxReturnInfo.NetValue,
                    nameof(TaxReturnInfo.NetValue),
                    nameof(TaxReturnInfo));
            }
        }

        private void ValidateVATValueIfSet()
        {
            if (_taxReturnInfo.VATValue != default && _taxReturnInfo.VATValue <= 0)
            {
                throw new ValueNotSupportedException(
                    _taxReturnInfo.VATValue,
                    nameof(TaxReturnInfo.VATValue),
                    nameof(TaxReturnInfo));
            }
        }

        private void ValidateAustrianVATRate()
        {
            if (_taxReturnInfo.AustrianVATRate == default)
            {
                throw new MissingRequiredPropertyException(
                    nameof(TaxReturnInfo.AustrianVATRate),
                    nameof(TaxReturnInfo));
            }
        }
    }
}
