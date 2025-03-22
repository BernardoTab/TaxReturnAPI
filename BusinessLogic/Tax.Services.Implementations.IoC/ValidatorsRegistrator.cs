using Microsoft.Extensions.DependencyInjection;
using Tax.Services.Implementations.Common.Validations;
using Tax.Services.Implementations.TaxReturns.Commands.Validators;

namespace Tax.Services.Implementations.IoC
{
    public static class ValidatorsRegistrator
    {
        public static void RegisterValidators(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<ProcessTaxReturnInfoCommandValidator>()
                .AddClasses(classes => classes.AssignableToAny(
                    typeof(ICommandValidator<,>),
                    typeof(IValueObjectValidator<>)))
                .AsImplementedInterfaces()
                .WithLifetime(ServiceLifetime.Transient));
        }
    }
}
