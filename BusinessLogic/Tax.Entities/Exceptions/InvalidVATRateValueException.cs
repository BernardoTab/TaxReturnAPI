using Tax.Entities.TaxReturns;

namespace Tax.Entities.Exceptions
{
    public class InvalidVATRateValueException : Exception, IKnownException
    {
        public object Value { get; set; }

        public InvalidVATRateValueException(object value)
            : base($"The value {value} of property {nameof(TaxReturnInfo.AustrianVATRate)} is not valid. " +
                $"The only valid inputs are '13', '15' and '20', representing the percentage. " +
                $"Alternatively you can also use VAT13Percent, VAT15Percent and VAT20Percent, respectively, as inputs.")
        {
            Value = value;
        }
    }
}
