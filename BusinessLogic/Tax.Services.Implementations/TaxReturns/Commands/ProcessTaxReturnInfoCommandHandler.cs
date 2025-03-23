using System.Reflection;
using Tax.Entities.TaxReturns;
using Tax.Services.Implementations.Common;
using Tax.Services.TaxReturns.Commands;

namespace Tax.Services.Implementations.TaxReturns.Commands
{
    public class ProcessTaxReturnInfoCommandHandler : ICommandHandler<ProcessTaxReturnInfoCommand, ProcessedTaxReturnInfo>
    {
        private ProcessTaxReturnInfoCommand _command;

        public async Task<ProcessedTaxReturnInfo> HandleAsync(ProcessTaxReturnInfoCommand command)
        {
            _command = command;
            ProcessedTaxReturnInfo processedTaxReturnInfo = ComputeTaxAmounts(_command.TaxReturnInfo);
            return await Task.FromResult(processedTaxReturnInfo);
        }

        private ProcessedTaxReturnInfo ComputeTaxAmounts(TaxReturnInfo taxReturnInfo)
        {
            string validAmountPropertyName = taxReturnInfo.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType == typeof(decimal?) && p.GetValue(taxReturnInfo) != null)
                .Select(p => p.Name)
                .First();
            ProcessedTaxReturnInfo result = new ProcessedTaxReturnInfo();
            switch (validAmountPropertyName)
            {
                case nameof(TaxReturnInfo.NetValue):
                    result = ProcessTaxInfoBasedOnNetValue();
                    break;
                case nameof(TaxReturnInfo.GrossValue):
                    result = ProcessTaxInfoBasedOnGrossValue();
                    break;
                case nameof(TaxReturnInfo.VATValue):
                    result = ProcessTaxInfoBasedOnVATValue();
                    break;
            }
            return result;
        }

        private ProcessedTaxReturnInfo ProcessTaxInfoBasedOnNetValue()
        {
            decimal? netValue = _command.TaxReturnInfo.NetValue;
            decimal vatRate = GetVATRate();
            decimal? vatValue = netValue * vatRate;
            return CreateTaxReturnInfo(
                grossValue: netValue!.Value + vatValue!.Value,
                netValue!.Value,
                vatValue!.Value);
        }

        private decimal GetVATRate()
        {
            return ((int)_command.TaxReturnInfo.AustrianVATRate) * 0.01m;
        }

        private ProcessedTaxReturnInfo CreateTaxReturnInfo(
            decimal grossValue,
            decimal netValue,
            decimal vatValue)
        {
            return new ProcessedTaxReturnInfo
            {
                GrossValue = Math.Round(grossValue, 2, MidpointRounding.ToEven),
                NetValue = Math.Round(netValue, 2, MidpointRounding.ToEven),
                VATValue = Math.Round(vatValue, 2, MidpointRounding.ToEven),
                AustrianVATRate = _command.TaxReturnInfo.AustrianVATRate
            };
        }

        private ProcessedTaxReturnInfo ProcessTaxInfoBasedOnGrossValue()
        {
            decimal? grossValue = _command.TaxReturnInfo.GrossValue;
            decimal vatRate = GetVATRate();
            decimal? vatValue = grossValue / (1 + 1 / vatRate);
            return CreateTaxReturnInfo(
                grossValue!.Value,
                netValue: grossValue!.Value - vatValue!.Value,
                vatValue!.Value);
        }

        private ProcessedTaxReturnInfo ProcessTaxInfoBasedOnVATValue()
        {
            decimal? vatValue = _command.TaxReturnInfo.VATValue;
            decimal vatRate = GetVATRate();
            decimal? netValue = vatValue / vatRate;
            return CreateTaxReturnInfo(
                grossValue: netValue!.Value + vatValue!.Value,
                netValue!.Value,
                vatValue!.Value);
        }
    }
}
