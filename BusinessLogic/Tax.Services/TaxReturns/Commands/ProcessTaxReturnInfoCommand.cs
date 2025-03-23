using Tax.Entities.TaxReturns;
using Tax.Services.Common;

namespace Tax.Services.TaxReturns.Commands
{
    public class ProcessTaxReturnInfoCommand : ICommand<ProcessedTaxReturnInfo>
    {
        public TaxReturnInfo TaxReturnInfo { get; set; }
    }
}
