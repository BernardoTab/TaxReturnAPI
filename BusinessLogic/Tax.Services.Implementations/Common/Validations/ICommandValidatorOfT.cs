using Tax.Services.Common;

namespace Tax.Services.Implementations.Common.Validations
{
    public interface ICommandValidator<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        Task ValidateAsync(TCommand command);
    }
}
