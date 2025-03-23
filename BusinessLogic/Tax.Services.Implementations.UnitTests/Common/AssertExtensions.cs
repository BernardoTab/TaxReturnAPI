using Tax.Entities.Exceptions;

namespace Tax.Services.Implementations.UnitTests.Common
{
    public static class AssertExtensions
    {
        public static async Task ThrowsMissingRequiredPropertyExceptionAsync(
            this Assert _,
            Func<Task> testAction,
            string propertyName)
        {
            MissingRequiredPropertyException exception =
                await Assert.ThrowsExceptionAsync<MissingRequiredPropertyException>(testAction);
            Assert.AreEqual(expected: propertyName, actual: exception.PropertyName);
        }

        public static async Task ThrowsIncorrectNumberOfValidAmountValuesExceptionAsync(
            this Assert _,
            Func<Task> testAction,
            int propertyCount)
        {
            IncorrectNumberOfValidAmountValuesException exception =
                await Assert.ThrowsExceptionAsync<IncorrectNumberOfValidAmountValuesException>(testAction);
            Assert.AreEqual(expected: propertyCount, actual: exception.PropertyCount);
        }

        public static async Task ThrowsValueNotSupportedExceptionAsync(
            this Assert _,
            Func<Task> testAction,
            object value,
            string propertyName)
        {
            ValueNotSupportedException exception =
                await Assert.ThrowsExceptionAsync<ValueNotSupportedException>(testAction);
            Assert.AreEqual(expected: propertyName, actual: exception.PropertyName);
            Assert.AreEqual(expected: value, actual: exception.Value);
        }

        public static async Task ThrowsInvalidVATRateValueExceptionAsync(
            this Assert _,
            Func<Task> testAction)
        {
            InvalidVATRateValueException exception =
                await Assert.ThrowsExceptionAsync<InvalidVATRateValueException>(testAction);
        }
    }
}
