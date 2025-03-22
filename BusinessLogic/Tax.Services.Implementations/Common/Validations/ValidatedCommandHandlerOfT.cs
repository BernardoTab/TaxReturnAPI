using Tax.Services.Common;

namespace Tax.Services.Implementations.Common.Validations
{
    public class ValidatedCommandHandler<TCommand, TResult> : ICommandHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        private readonly ICommandValidator<TCommand, TResult> _commandValidator;
        private readonly ICommandHandler<TCommand, TResult> _commandHandler;

        public ValidatedCommandHandler(
            ICommandValidator<TCommand, TResult> commandValidator,
            ICommandHandler<TCommand, TResult> commandHandler)
        {
            _commandValidator = commandValidator;
            _commandHandler = commandHandler;
        }

        public async Task<TResult> HandleAsync(TCommand command)
        {
            await _commandValidator.ValidateAsync(command);
            return await _commandHandler.HandleAsync(command);
        }
    }
}
