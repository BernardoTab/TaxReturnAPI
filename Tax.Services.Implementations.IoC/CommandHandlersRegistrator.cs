using Microsoft.Extensions.DependencyInjection;
using Tax.Services.Implementations.Common;
using Tax.Services.Implementations.TaxReturns.Commands;

namespace Tax.Services.Implementations.IoC
{
    public static class CommandHandlersRegistrator
    {
        public static void RegisterCommandHandlers(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<ProcessTaxReturnInfoCommandHandler>()
                .AddClasses(classes => classes.AssignableToAny(typeof(ICommandHandler<,>)))
                .AsImplementedInterfaces()
                .WithLifetime(ServiceLifetime.Transient));
        }
    }
}
