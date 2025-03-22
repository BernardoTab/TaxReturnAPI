namespace Tax.Services.Implementations.Common.Validations
{
    public interface IValueObjectValidator<T> where T : class
    {
        Task ValidateAsync(T valueObject);
    }
}
