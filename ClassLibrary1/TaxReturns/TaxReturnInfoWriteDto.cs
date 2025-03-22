namespace Tax.DataTransferring.TaxReturns
{
    public class TaxReturnInfoWriteDto
    {
        public decimal? GrossValue { get; set; }
        public decimal? NetValue { get; set; }
        public decimal? VATValue { get; set; }
        public VATRateDto AustrianVATRate { get; set; }
    }
}
