using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Tax.DataTransferring.TaxReturns;
using Tax.Entities.Exceptions;

namespace Tax.AcceptanceTests.Controllers
{
    [TestClass]
    [DoNotParallelize]
    public class TaxesControllerTests
    {
        private static WebApplicationFactory<Program> _factory;
        private static HttpClient _client;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        [TestMethod]
        [DataRow(VATRateDto.VAT10Percent)]
        [DataRow(VATRateDto.VAT13Percent)]
        [DataRow(VATRateDto.VAT20Percent)]
        public async Task ProcessTaxReturnInfoAsync_GrossValueIsSet_DifferentVATRates_ReturnsSuccess_TaxReturnInfoFilledCorrectly(
            VATRateDto vatRate)
        {
            // Arrange
            TaxReturnInfoWriteDto expectedTaxReturnInfo = new TaxReturnInfoWriteDto
            {
                GrossValue = 10.5m,
                AustrianVATRate = vatRate.ToString()
            };
            (decimal expectedGrossValue, decimal expectedNetValue, decimal expectedVATValue) = GetExpectedAmountValuesBasedOnFixedGross(vatRate);

            // Act
            TaxReturnInfoReadDto result = await ProcessTaxReturnInfoAsync(expectedTaxReturnInfo);

            // Assert
            expectedTaxReturnInfo.Should().BeEquivalentTo(
                result,
                o => o.Excluding(tri => tri.NetValue)
                    .Excluding(tri => tri.VATValue)
                    .Excluding(tri => tri.AustrianVATRate));
            Assert.AreEqual(expectedTaxReturnInfo.AustrianVATRate, result.AustrianVATRate.ToString());
            Assert.AreEqual(expectedVATValue, result.VATValue);
            Assert.AreEqual(expectedNetValue, result.NetValue);
        }

        private (decimal expectedGrossValue, decimal expectedNetValue, decimal expectedVATValue) GetExpectedAmountValuesBasedOnFixedGross(VATRateDto vatRate)
        {
            switch (vatRate)
            {
                default:
                case VATRateDto.VAT10Percent:
                    return (10.5m, 9.55m, 0.95m);
                case VATRateDto.VAT13Percent:
                    return (10.5m, 9.29m, 1.21m);
                case VATRateDto.VAT20Percent:
                    return (10.5m, 8.75m, 1.75m);
            }
        }

        private async Task<TaxReturnInfoReadDto> ProcessTaxReturnInfoAsync(TaxReturnInfoWriteDto taxReturnInfo)
        {
            HttpResponseMessage response = await PostAsync(taxReturnInfo);
            string responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TaxReturnInfoReadDto>(responseBody)!;
        }

        private async Task<HttpResponseMessage> PostAsync(TaxReturnInfoWriteDto taxReturnInfo)
        {
            string endpointUri = $"/api/Taxes/";
            string jsonPayload = JsonConvert.SerializeObject(taxReturnInfo);
            HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            return await _client.PostAsync(endpointUri, content);
        }

        [TestMethod]
        [DataRow(VATRateDto.VAT10Percent)]
        [DataRow(VATRateDto.VAT13Percent)]
        [DataRow(VATRateDto.VAT20Percent)]
        public async Task ProcessTaxReturnInfoAsync_NetValueIsSet_DifferentVATRates_ReturnsSuccess_TaxReturnInfoFilledCorrectly(
            VATRateDto vatRate)
        {
            TaxReturnInfoWriteDto expectedTaxReturnInfo = new TaxReturnInfoWriteDto
            {
                NetValue = 10.5m,
                AustrianVATRate = vatRate.ToString()
            };
            (decimal expectedGrossValue, decimal expectedNetValue, decimal expectedVATValue) = GetExpectedAmountValuesBasedOnFixedNet(vatRate);

            TaxReturnInfoReadDto result = await ProcessTaxReturnInfoAsync(expectedTaxReturnInfo);

            expectedTaxReturnInfo.Should().BeEquivalentTo(
                result,
                o => o.Excluding(tri => tri.GrossValue)
                    .Excluding(tri => tri.VATValue)
                    .Excluding(tri => tri.AustrianVATRate));
            Assert.AreEqual(expectedTaxReturnInfo.AustrianVATRate, result.AustrianVATRate.ToString());
            Assert.AreEqual(expectedVATValue, result.VATValue);
            Assert.AreEqual(expectedGrossValue, result.GrossValue);
        }

        private (decimal expectedGrossValue, decimal expectedNetValue, decimal expectedVATValue) GetExpectedAmountValuesBasedOnFixedNet(VATRateDto vatRate)
        {
            switch (vatRate)
            {
                default:
                case VATRateDto.VAT10Percent:
                    return (11.55m, 10.5m, 1.05m);
                case VATRateDto.VAT13Percent:
                    return (11.86m, 10.5m, 1.36m);
                case VATRateDto.VAT20Percent:
                    return (12.6m, 10.5m, 2.10m);
            }
        }

        [TestMethod]
        [DataRow(VATRateDto.VAT10Percent)]
        [DataRow(VATRateDto.VAT13Percent)]
        [DataRow(VATRateDto.VAT20Percent)]
        public async Task ProcessTaxReturnInfoAsync_VatValueIsSet_DifferentVATRates_ReturnsSuccess_TaxReturnInfoFilledCorrectly(
            VATRateDto vatRate)
        {
            TaxReturnInfoWriteDto expectedTaxReturnInfo = new TaxReturnInfoWriteDto
            {
                VATValue = 10.5m,
                AustrianVATRate = vatRate.ToString()
            };
            (decimal expectedGrossValue, decimal expectedNetValue, decimal expectedVATValue) = GetExpectedAmountValuesBasedOnFixedVatValue(vatRate);

            TaxReturnInfoReadDto result = await ProcessTaxReturnInfoAsync(expectedTaxReturnInfo);

            expectedTaxReturnInfo.Should().BeEquivalentTo(
                result,
                o => o.Excluding(tri => tri.GrossValue)
                    .Excluding(tri => tri.NetValue)
                    .Excluding(tri => tri.AustrianVATRate));
            Assert.AreEqual(expectedTaxReturnInfo.AustrianVATRate, result.AustrianVATRate.ToString());
            Assert.AreEqual(expectedNetValue, result.NetValue);
            Assert.AreEqual(expectedGrossValue, result.GrossValue);
        }

        private (decimal expectedGrossValue, decimal expectedNetValue, decimal expectedVATValue) GetExpectedAmountValuesBasedOnFixedVatValue(VATRateDto vatRate)
        {
            switch (vatRate)
            {
                default:
                case VATRateDto.VAT10Percent:
                    return (115.5m, 105.0m, 10.5m);
                case VATRateDto.VAT13Percent:
                    return (91.27m, 80.77m, 10.5m);
                case VATRateDto.VAT20Percent:
                    return (63.0m, 52.5m, 10.5m);
            }
        }

        [TestMethod]
        public async Task ProcessTaxReturnInfoAsync_NoAmountParameters_ExceptionIsThrown()
        {
            TaxReturnInfoWriteDto expectedTaxReturnInfo = new TaxReturnInfoWriteDto
            {
                AustrianVATRate = VATRateDto.VAT13Percent.ToString()
            };

            HttpResponseMessage response = await PostAsync(expectedTaxReturnInfo);

            ErrorResponse? jsonResponse = await RetrieveErrorResponseOrDefault(response);
            jsonResponse.Should().NotBeNull();
            jsonResponse!.ExceptionType.Should().Be(nameof(IncorrectNumberOfValidAmountValuesException));
        }

        private class ErrorResponse
        {
            public int StatusCode { get; set; }
            public string Message { get; set; }
            public string ExceptionType { get; set; }
        }

        private async Task<ErrorResponse?> RetrieveErrorResponseOrDefault(HttpResponseMessage response)
        {
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
            string jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ErrorResponse>(jsonString);
        }

        [TestMethod]
        public async Task ProcessTaxReturnInfoAsync_MoreThanOneAmountParameters_ExceptionIsThrown()
        {
            TaxReturnInfoWriteDto expectedTaxReturnInfo = new TaxReturnInfoWriteDto
            {
                GrossValue = 50.4m,
                NetValue = 12.4m,
                AustrianVATRate = VATRateDto.VAT13Percent.ToString()
            };

            HttpResponseMessage response = await PostAsync(expectedTaxReturnInfo);

            ErrorResponse? jsonResponse = await RetrieveErrorResponseOrDefault(response);
            jsonResponse.Should().NotBeNull();
            jsonResponse!.ExceptionType.Should().Be(nameof(IncorrectNumberOfValidAmountValuesException));
        }

        [TestMethod]
        public async Task ProcessTaxReturnInfoAsync_MissingAustrianVATRate_ExceptionIsThrown()
        {
            TaxReturnInfoWriteDto expectedTaxReturnInfo = new TaxReturnInfoWriteDto
            {
                GrossValue = 50.4m,
            };

            HttpResponseMessage response = await PostAsync(expectedTaxReturnInfo);

            ErrorResponse? jsonResponse = await RetrieveErrorResponseOrDefault(response);
            jsonResponse.Should().NotBeNull();
            jsonResponse!.ExceptionType.Should().Be(nameof(InvalidVATRateValueException));
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-5.7)]
        public async Task ProcessTaxReturnInfoAsync_GrossValueIsSet_InvalidValueAmount_ExceptionIsThrown(double value)
        {
            TaxReturnInfoWriteDto expectedTaxReturnInfo = new TaxReturnInfoWriteDto
            {
                GrossValue = (decimal)value,
                AustrianVATRate = VATRateDto.VAT13Percent.ToString(),
            };

            HttpResponseMessage response = await PostAsync(expectedTaxReturnInfo);

            ErrorResponse? jsonResponse = await RetrieveErrorResponseOrDefault(response);
            jsonResponse.Should().NotBeNull();
            jsonResponse!.ExceptionType.Should().Be(nameof(ValueNotSupportedException));
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-5.7)]
        public async Task ProcessTaxReturnInfoAsync_NetValueIsSet_InvalidValueAmount_ExceptionIsThrown(double value)
        {
            TaxReturnInfoWriteDto expectedTaxReturnInfo = new TaxReturnInfoWriteDto
            {
                NetValue = (decimal)value,
                AustrianVATRate = VATRateDto.VAT13Percent.ToString()
            };

            HttpResponseMessage response = await PostAsync(expectedTaxReturnInfo);

            ErrorResponse? jsonResponse = await RetrieveErrorResponseOrDefault(response);
            jsonResponse.Should().NotBeNull();
            jsonResponse!.ExceptionType.Should().Be(nameof(ValueNotSupportedException));
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-5.7)]
        public async Task ProcessTaxReturnInfoAsync_VatValueIsSet_InvalidValueAmount_ExceptionIsThrown(double value)
        {
            TaxReturnInfoWriteDto expectedTaxReturnInfo = new TaxReturnInfoWriteDto
            {
                VATValue = (decimal)value,
                AustrianVATRate = VATRateDto.VAT13Percent.ToString()
            };

            HttpResponseMessage response = await PostAsync(expectedTaxReturnInfo);

            ErrorResponse? jsonResponse = await RetrieveErrorResponseOrDefault(response);
            jsonResponse.Should().NotBeNull();
            jsonResponse!.ExceptionType.Should().Be(nameof(ValueNotSupportedException));
        }

        [TestMethod]
        public async Task ProcessTaxReturnInfoAsync_AustrianVatRateIsSet_InvalidFormat_ExceptionIsThrown()
        {
            string incorrectParameterTypePayload = @"
            {
                ""GrossValue"": 1000.50,
                ""AustrianVATRate"": ""snoopy""
            }";

            HttpResponseMessage response = await PostAsyncWithString(incorrectParameterTypePayload);

            ErrorResponse? jsonResponse = await RetrieveErrorResponseOrDefault(response);
            jsonResponse.Should().NotBeNull();
            jsonResponse!.ExceptionType.Should().Be(nameof(InvalidVATRateValueException));
        }

        private async Task<HttpResponseMessage> PostAsyncWithString(string jsonPayload)
        {
            string endpointUri = $"/api/Taxes/";
            HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            return await _client.PostAsync(endpointUri, content);
        }
    }
}
