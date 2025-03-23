using Tax.Services.Implementations.Common.Validations;

namespace Tax.Services.Implementations.UnitTests.Common
{
    public abstract class ValueObjectValidatorTests<TValueObjectValidator, TValueObject>
        where TValueObjectValidator : IValueObjectValidator<TValueObject>
        where TValueObject : class
    {
        protected TValueObjectValidator ValueObjectValidator { get; set; }
        protected TValueObject ValueObject { get; set; }

        protected ValueObjectValidatorTests()
        {
            InitializeDependencyMocks();
            ValueObjectValidator = CreateValidator();
            ValueObject = CreateValidEntity();
            SetupDependencyMocks();
        }

        protected virtual void InitializeDependencyMocks()
        {
        }

        protected abstract TValueObjectValidator CreateValidator();

        protected abstract TValueObject CreateValidEntity();

        protected virtual void SetupDependencyMocks()
        {
        }
    }
}
