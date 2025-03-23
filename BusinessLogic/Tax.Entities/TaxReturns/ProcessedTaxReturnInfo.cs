namespace Tax.Entities.TaxReturns
{
    public class ProcessedTaxReturnInfo
    {
        public decimal GrossValue { get; set; }
        public decimal NetValue { get; set; }
        public decimal VATValue { get; set; }
        public VATRate AustrianVATRate { get; set; }
    }
}
