using Microsoft.AspNetCore.Mvc;
using Tax.DataTransferring.ModelBinding;

namespace Tax.DataTransferring.TaxReturns
{
    public class TaxReturnInfoWriteDto
    {
        [ModelBinder(BinderType = typeof(DecimalModelBinder))]
        public decimal? GrossValue { get; set; }
        [ModelBinder(BinderType = typeof(DecimalModelBinder))]
        public decimal? NetValue { get; set; }
        [ModelBinder(BinderType = typeof(DecimalModelBinder))]
        public decimal? VATValue { get; set; }
        [ModelBinder(BinderType = typeof(VATRateEnumModelBinder))]
        public VATRateDto AustrianVATRate { get; set; }
    }
}
