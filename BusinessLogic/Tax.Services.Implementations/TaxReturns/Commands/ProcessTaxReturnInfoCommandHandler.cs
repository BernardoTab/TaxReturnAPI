using System.Reflection;
using Tax.Entities.TaxReturns;
using Tax.Services.Implementations.Common;
using Tax.Services.TaxReturns.Commands;

namespace Tax.Services.Implementations.TaxReturns.Commands
{
    public class ProcessTaxReturnInfoCommandHandler : ICommandHandler<ProcessTaxReturnInfoCommand, TaxReturnInfo>
    {
        private ProcessTaxReturnInfoCommand _command;

        public async Task<TaxReturnInfo> HandleAsync(ProcessTaxReturnInfoCommand command)
        {
            _command = command;
            TaxReturnInfo processedTaxReturnInfo = ComputeTaxAmounts(_command.TaxReturnInfo);
            return await Task.FromResult(processedTaxReturnInfo);
        }

        private TaxReturnInfo ComputeTaxAmounts(TaxReturnInfo taxReturnInfo)
        {
            string validAmountPropertyName = taxReturnInfo.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType == typeof(decimal?) && p.GetValue(taxReturnInfo) != null)
                .Select(p => p.Name)
                .First();
            TaxReturnInfo result = new TaxReturnInfo();
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

        private TaxReturnInfo ProcessTaxInfoBasedOnNetValue()
        {
            decimal? netValue = _command.TaxReturnInfo.NetValue;
            decimal vatRate = GetVATRate();
            decimal? vatValue = netValue / vatRate;
            return CreateTaxReturnInfo(
                grossValue: netValue + vatValue,
                netValue,
                vatValue);
        }

        private decimal GetVATRate()
        {
            return ((int)_command.TaxReturnInfo.AustrianVATRate) * 0.01m;
        }

        private TaxReturnInfo CreateTaxReturnInfo(
            decimal? grossValue,
            decimal? netValue,
            decimal? vatValue)
        {
            return new TaxReturnInfo
            {
                GrossValue = grossValue,
                NetValue = netValue,
                VATValue = vatValue,
                AustrianVATRate = _command.TaxReturnInfo.AustrianVATRate
            };
        }

        private TaxReturnInfo ProcessTaxInfoBasedOnGrossValue()
        {
            decimal? grossValue = _command.TaxReturnInfo.GrossValue;
            decimal vatRate = GetVATRate();
            decimal? vatValue = grossValue * vatRate;
            return CreateTaxReturnInfo(
                grossValue,
                netValue: grossValue - vatValue,
                vatValue);
        }

        private TaxReturnInfo ProcessTaxInfoBasedOnVATValue()
        {
            decimal? vatValue = _command.TaxReturnInfo.VATValue;
            decimal vatRate = GetVATRate();
            decimal? grossValue = vatValue / vatRate;
            return CreateTaxReturnInfo(
                grossValue,
                netValue: grossValue - vatValue,
                vatValue);
        }
    }
}
