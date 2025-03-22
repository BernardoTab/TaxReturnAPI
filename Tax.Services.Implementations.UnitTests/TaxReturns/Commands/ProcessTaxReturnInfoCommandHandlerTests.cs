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
            TaxReturnInfo>
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
        [DataRow(VATRate.VAT13Percent)]
        [DataRow(VATRate.VAT15Percent)]
        [DataRow(VATRate.VAT20Percent)]
        public async Task HandleAsync_GrossValueIsSet_TaxReturnInfoIsProcessedCorrectly(VATRate vatRate)
        {
            Command.TaxReturnInfo.AustrianVATRate = vatRate;
            decimal expectedVatRate = GetVATRate();
            decimal? expectedVatValue = Command.TaxReturnInfo.GrossValue * expectedVatRate;
            decimal? expectedNetValue = Command.TaxReturnInfo.GrossValue - expectedVatValue;

            TaxReturnInfo result = await CommandHandler.HandleAsync(Command);

            Assert.AreEqual(Command.TaxReturnInfo.GrossValue, result.GrossValue);
            Assert.AreEqual(expectedNetValue, result.NetValue);
            Assert.AreEqual(expectedVatValue, result.VATValue);
            Assert.AreEqual(Command.TaxReturnInfo.AustrianVATRate, result.AustrianVATRate);
        }

        private decimal GetVATRate()
        {
            return ((int)Command.TaxReturnInfo.AustrianVATRate) * 0.01m;
        }

        [TestMethod]
        [DataRow(VATRate.VAT13Percent)]
        [DataRow(VATRate.VAT15Percent)]
        [DataRow(VATRate.VAT20Percent)]
        public async Task HandleAsync_NetValueIsSet_TaxReturnInfoIsProcessedCorrectly(VATRate vatRate)
        {
            Command.TaxReturnInfo.AustrianVATRate = vatRate;
            Command.TaxReturnInfo.GrossValue = default;
            Command.TaxReturnInfo.NetValue = 50;
            decimal expectedVatRate = GetVATRate();
            decimal? expectedVatValue = Command.TaxReturnInfo.NetValue / expectedVatRate;
            decimal? expectedGrossValue = Command.TaxReturnInfo.NetValue + expectedVatValue;

            TaxReturnInfo result = await CommandHandler.HandleAsync(Command);

            Assert.AreEqual(Command.TaxReturnInfo.NetValue, result.NetValue);
            Assert.AreEqual(expectedGrossValue, result.GrossValue);
            Assert.AreEqual(expectedVatValue, result.VATValue);
            Assert.AreEqual(Command.TaxReturnInfo.AustrianVATRate, result.AustrianVATRate);
        }

        [TestMethod]
        [DataRow(VATRate.VAT13Percent)]
        [DataRow(VATRate.VAT15Percent)]
        [DataRow(VATRate.VAT20Percent)]
        public async Task HandleAsync_VATValueIsSet_TaxReturnInfoIsProcessedCorrectly(VATRate vatRate)
        {
            Command.TaxReturnInfo.AustrianVATRate = vatRate;
            Command.TaxReturnInfo.GrossValue = default;
            Command.TaxReturnInfo.VATValue = 50;
            decimal expectedVatRate = GetVATRate();
            decimal? expectedGrossValue = Command.TaxReturnInfo.VATValue / expectedVatRate;
            decimal? expectedNetValue = expectedGrossValue - Command.TaxReturnInfo.VATValue;

            TaxReturnInfo result = await CommandHandler.HandleAsync(Command);

            Assert.AreEqual(Command.TaxReturnInfo.VATValue, result.VATValue);
            Assert.AreEqual(expectedGrossValue, result.GrossValue);
            Assert.AreEqual(expectedNetValue, result.NetValue);
            Assert.AreEqual(Command.TaxReturnInfo.AustrianVATRate, result.AustrianVATRate);
        }
    }
}
