using Tax.Entities.TaxReturns;

namespace Tax.Entities.Exceptions
{
    public class IncorrectNumberOfValidAmountValuesException : Exception
    {
        public int PropertyCount { get; set; }

        public IncorrectNumberOfValidAmountValuesException(int propertyCount)
            : base($"Only one of the following input properties: '{nameof(TaxReturnInfo.GrossValue)}', '{nameof(TaxReturnInfo.NetValue)}' and '{nameof(TaxReturnInfo.VATValue)}' should be set, but {propertyCount} were given as input.")
        {
            PropertyCount = propertyCount;
        }
    }
}
