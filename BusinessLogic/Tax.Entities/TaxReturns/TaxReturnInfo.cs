namespace Tax.Entities.TaxReturns
{
    public class TaxReturnInfo
    {
        public decimal? GrossValue { get; set; }
        public decimal? NetValue { get; set; }
        public decimal? VATValue { get; set; }
        public VATRate AustrianVATRate { get; set; }
    }
}
