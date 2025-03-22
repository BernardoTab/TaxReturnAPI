using Tax.DataTransferring.Entities.Mapping.TaxReturns;
using Tax.Services.Implementations.IoC;

namespace TaxReturnAPI
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddControllers();
            serviceCollection.AddSwaggerGen();
            serviceCollection.AddAutoMapper(typeof(VATRateDtoMap).Assembly);
            serviceCollection.RegisterCommandHandlers();
            serviceCollection.RegisterValidators();
        }
    }
}
