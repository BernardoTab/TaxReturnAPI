using AutoMapper;
using Tax.DataTransferring.TaxReturns;
using Tax.Entities.TaxReturns;

namespace Tax.DataTransferring.Entities.Mapping.TaxReturns
{
    public class TaxReturnInfoDtoMap : Profile
    {
        public TaxReturnInfoDtoMap()
        {
            CreateMap<TaxReturnInfoWriteDto, TaxReturnInfo>();
            CreateMap<TaxReturnInfo, TaxReturnInfoReadDto>();
        }
    }
}
