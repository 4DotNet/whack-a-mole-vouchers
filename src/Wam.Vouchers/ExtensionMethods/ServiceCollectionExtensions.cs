using Microsoft.Extensions.DependencyInjection;

namespace Wam.Vouchers.ExtensionMethods;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWamUsersModule(this IServiceCollection services)
    {
        return services;
    }
}