using Tax.Entities.TaxReturns;
using Tax.Services.Common;

namespace Tax.Services.TaxReturns.Commands
{
    public class ProcessTaxReturnInfoCommand : ICommand<TaxReturnInfo>
    {
        public TaxReturnInfo TaxReturnInfo { get; set; }
    }
}
