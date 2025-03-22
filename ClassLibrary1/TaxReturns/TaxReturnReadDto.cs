namespace Tax.DataTransferring.TaxReturns
{
    public class TaxReturnReadDto
    {
        public decimal GrossValue { get; set; }
        public decimal NetValue { get; set; }
        public decimal VATValue { get; set; }
    }
}
