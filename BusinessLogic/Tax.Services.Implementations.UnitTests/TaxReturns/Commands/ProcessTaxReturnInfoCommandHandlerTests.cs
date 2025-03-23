using Tax.Entities.TaxReturns;
using Tax.Services.Implementations.TaxReturns.Commands;
using Tax.Services.Implementations.UnitTests.Common;
using Tax.Services.TaxReturns.Commands;

namespace Tax.Services.Implementations.UnitTests.TaxReturns.Commands
{
    [TestClass]
    [DoNotParallelize]
    public class ProcessTaxReturnInfoCommandHandlerTests :
        CommandHandlerTests<
            ProcessTaxReturnInfoCommandHandler,
            ProcessTaxReturnInfoCommand,
            ProcessedTaxReturnInfo>
    {
        protected override ProcessTaxReturnInfoCommand CreateValidCommand()
        {
            return new ProcessTaxReturnInfoCommand
            {
                TaxReturnInfo = new TaxReturnInfo
                {
                    GrossValue = 50,
                    AustrianVATRate = VATRate.VAT13Percent
                }
            };
        }

        protected override ProcessTaxReturnInfoCommandHandler InitializeCommandHandler()
        {
            return new ProcessTaxReturnInfoCommandHandler();
        }

        protected override void InitializeDependenciesMocks()
        {
        }

        [TestMethod]
        [DataRow(VATRate.VAT10Percent)]
        [DataRow(VATRate.VAT13Percent)]
        [DataRow(VATRate.VAT20Percent)]
        public async Task HandleAsync_GrossValueIsSet_TaxReturnInfoIsProcessedCorrectly(VATRate vatRate)
        {
            Command.TaxReturnInfo.AustrianVATRate = vatRate;
            decimal expectedVatRate = GetVATRate();
            decimal? expectedVatValue = Command.TaxReturnInfo.GrossValue / (1 + 1 / expectedVatRate);
            decimal? expectedNetValue = Command.TaxReturnInfo.GrossValue - expectedVatValue;

            ProcessedTaxReturnInfo result = await CommandHandler.HandleAsync(Command);

            Assert.AreEqual(Command.TaxReturnInfo.GrossValue, result.GrossValue);
            Assert.AreEqual(Math.Round(expectedNetValue!.Value, 2, MidpointRounding.ToEven), result.NetValue);
            Assert.AreEqual(Math.Round(expectedVatValue!.Value, 2, MidpointRounding.ToEven), result.VATValue);
            Assert.AreEqual(Command.TaxReturnInfo.AustrianVATRate, result.AustrianVATRate);
        }

        private decimal GetVATRate()
        {
            return ((int)Command.TaxReturnInfo.AustrianVATRate) * 0.01m;
        }

        [TestMethod]
        [DataRow(VATRate.VAT10Percent)]
        [DataRow(VATRate.VAT13Percent)]
        [DataRow(VATRate.VAT20Percent)]
        public async Task HandleAsync_NetValueIsSet_TaxReturnInfoIsProcessedCorrectly(VATRate vatRate)
        {
            Command.TaxReturnInfo.AustrianVATRate = vatRate;
            Command.TaxReturnInfo.GrossValue = default;
            Command.TaxReturnInfo.NetValue = 50;
            decimal expectedVatRate = GetVATRate();
            decimal? expectedVatValue = Command.TaxReturnInfo.NetValue * expectedVatRate;
            decimal? expectedGrossValue = Command.TaxReturnInfo.NetValue + expectedVatValue;

            ProcessedTaxReturnInfo result = await CommandHandler.HandleAsync(Command);

            Assert.AreEqual(Command.TaxReturnInfo.NetValue, result.NetValue);
            Assert.AreEqual(Math.Round(expectedGrossValue!.Value, 2, MidpointRounding.ToEven), result.GrossValue);
            Assert.AreEqual(Math.Round(expectedVatValue!.Value, 2, MidpointRounding.ToEven), result.VATValue);
            Assert.AreEqual(Command.TaxReturnInfo.AustrianVATRate, result.AustrianVATRate);
        }

        [TestMethod]
        [DataRow(VATRate.VAT10Percent)]
        [DataRow(VATRate.VAT13Percent)]
        [DataRow(VATRate.VAT20Percent)]
        public async Task HandleAsync_VATValueIsSet_TaxReturnInfoIsProcessedCorrectly(VATRate vatRate)
        {
            Command.TaxReturnInfo.AustrianVATRate = vatRate;
            Command.TaxReturnInfo.GrossValue = default;
            Command.TaxReturnInfo.VATValue = 50;
            decimal expectedVatRate = GetVATRate();
            decimal? expectedNetValue = Command.TaxReturnInfo.VATValue / expectedVatRate;
            decimal? expectedGrossValue = expectedNetValue + Command.TaxReturnInfo.VATValue;

            ProcessedTaxReturnInfo result = await CommandHandler.HandleAsync(Command);

            Assert.AreEqual(Command.TaxReturnInfo.VATValue, result.VATValue);
            Assert.AreEqual(Math.Round(expectedGrossValue!.Value, 2, MidpointRounding.ToEven), result.GrossValue);
            Assert.AreEqual(Math.Round(expectedNetValue!.Value, 2, MidpointRounding.ToEven), result.NetValue);
            Assert.AreEqual(Command.TaxReturnInfo.AustrianVATRate, result.AustrianVATRate);
        }
    }
}
