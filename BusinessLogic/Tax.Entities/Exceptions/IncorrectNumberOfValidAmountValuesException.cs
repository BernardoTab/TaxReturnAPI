using Tax.Entities.TaxReturns;

namespace Tax.Entities.Exceptions
{
    public class IncorrectNumberOfValidAmountValuesException : Exception, IKnownException
    {
        public int PropertyCount { get; set; }

        public IncorrectNumberOfValidAmountValuesException(int propertyCount)
            : base($"Incorrect number of amount inputs were given. " +
                $"You should set one and only one of the following input properties: '{nameof(TaxReturnInfo.GrossValue)}', '{nameof(TaxReturnInfo.NetValue)}' or '{nameof(TaxReturnInfo.VATValue)}', however {propertyCount} of these were given as input.")
        {
            PropertyCount = propertyCount;
        }
    }
}
