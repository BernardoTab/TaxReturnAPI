using AutoMapper;
using Tax.DataTransferring.TaxReturns;
using Tax.Entities.TaxReturns;

namespace Tax.DataTransferring.Entities.Mapping.TaxReturns
{
    public class TaxReturnInfoDtoMap : Profile
    {
        public TaxReturnInfoDtoMap()
        {
            CreateMap<TaxReturnInfoWriteDto, TaxReturnInfo>()
                .ForMember(dest => dest.AustrianVATRate, opt => opt.MapFrom(src =>
                    ConvertToVATRate(src.AustrianVATRate)));
            CreateMap<ProcessedTaxReturnInfo, TaxReturnInfoReadDto>();
        }

        private VATRate ConvertToVATRate(string vatRateStr)
        {
            return Enum.TryParse(typeof(VATRate), vatRateStr, true, out object? vatRate) && Enum.IsDefined(typeof(VATRate), vatRate)
                ? (VATRate)vatRate
                : VATRate.Unknown;
        }
    }
}
