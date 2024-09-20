using Dapr.Client;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Logging;
using Wam.Vouchers.Repositories;

namespace Wam.Vouchers.Services;

public class VouchersService(
    IVouchersRepository repository,
    ILogger<VouchersService> logger,
    TelemetryClient telemetry,
    DaprClient daprClient) : IVouchersService
{

    private const string StateStoreName = "statestore";

    private readonly Dictionary<string, string> defaultCacheMetaData = new()
    {
        {
            "ttlInSeconds", "900"
        }
    };


}