using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PwCustomer.Application;

namespace PwCustomer.Infrastructure;

public static class CustomerDI
{
    public static IServiceCollection AddInfraServices(this IServiceCollection services)
    {
        services.AddDbContext<CustomerDbContext>(
                    Options => Options.UseInMemoryDatabase(Guid.NewGuid().ToString())
                    , ServiceLifetime.Scoped, ServiceLifetime.Singleton);

        services.AddTransient<ICustomerRepository, CustomerRepository>();
        return services;
    }
}
