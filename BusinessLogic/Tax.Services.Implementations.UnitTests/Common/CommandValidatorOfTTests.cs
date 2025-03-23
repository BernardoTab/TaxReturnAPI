using Tax.Services.Common;
using Tax.Services.Implementations.Common.Validations;

namespace Tax.Services.Implementations.UnitTests.Common
{
    public abstract class CommandValidatorTests<TCommandValidator, TCommand, TResult>
        where TCommandValidator : ICommandValidator<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        protected TCommandValidator CommandValidator { get; set; }
        protected TCommand Command { get; set; }

        protected CommandValidatorTests()
        {
            InitializeDependencyMocks();
            CommandValidator = CreateValidator();
            Command = CreateValidCommand();
            SetupDependencyMocks();
        }

        protected virtual void InitializeDependencyMocks()
        {
        }

        protected abstract TCommandValidator CreateValidator();

        protected abstract TCommand CreateValidCommand();

        protected virtual void SetupDependencyMocks()
        {
        }

        [TestMethod]
        public async Task ValidateAsync_CommandIsDefault_ArgumentNullExceptionIsThrown()
        {
            Command = default;

            Func<Task> testAction = async () => await CommandValidator.ValidateAsync(Command);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(testAction);
        }
    }
}
