using Tax.Entities.TaxReturns;

namespace Tax.Entities.Exceptions
{
    public class InvalidVATRateValueException : Exception, IKnownException
    {
        public InvalidVATRateValueException()
            : base($"The value of property {nameof(TaxReturnInfo.AustrianVATRate)} is not valid. " +
                $"The only valid inputs are 10, 13 and 20, representing the percentage. " +
                $"Alternatively you can also use VAT10Percent, VAT13Percent and VAT20Percent, respectively, as inputs.")
        {
        }
    }
}
