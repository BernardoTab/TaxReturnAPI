using Tax.Entities.TaxReturns;
using Tax.Services.Implementations.Common;
using Tax.Services.TaxReturns.Commands;

namespace Tax.Services.Implementations.TaxReturns.Commands
{
    public class ProcessTaxReturnInfoCommandHandler : ICommandHandler<ProcessTaxReturnInfoCommand, TaxReturnInfo>
    {
        public async Task<TaxReturnInfo> HandleAsync(ProcessTaxReturnInfoCommand command)
        {
            return await Task.FromResult(new TaxReturnInfo
            {
                NetValue = 5,
                GrossValue = 10,
                VATValue = 11,
                AustrianVATRate = VATRate.VAT13Percent
            });
        }
    }
}
