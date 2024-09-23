using Microsoft.Extensions.DependencyInjection;
using Wam.Vouchers.Repositories;
using Wam.Vouchers.Services;

namespace Wam.Vouchers.ExtensionMethods;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWamVouchersModule(this IServiceCollection services)
    {
        services.AddScoped<IVouchersRepository, VouchersRepository>();
        services.AddScoped<IVouchersService, VouchersService>();
        return services;
    }
}