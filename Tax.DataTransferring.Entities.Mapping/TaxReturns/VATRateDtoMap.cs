using AutoMapper;
using Tax.DataTransferring.TaxReturns;
using Tax.Services.TaxReturns.Commands;

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
