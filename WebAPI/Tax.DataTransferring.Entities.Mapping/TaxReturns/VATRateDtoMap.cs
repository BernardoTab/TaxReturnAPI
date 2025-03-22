using AutoMapper;
using Tax.DataTransferring.TaxReturns;
using Tax.Entities.TaxReturns;

namespace Tax.DataTransferring.Entities.Mapping.TaxReturns
{
    public class VATRateDtoMap : Profile
    {
        public VATRateDtoMap()
        {
            CreateMap<VATRateDto, VATRate>()
                .ReverseMap();
        }
    }
}
