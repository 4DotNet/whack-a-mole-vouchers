using Azure;
using Azure.Data.Tables;
using Microsoft.Extensions.Options;
using Wam.Core.Configuration;
using Wam.Core.Identity;
using Wam.Vouchers.Entities;

namespace Wam.Vouchers.Repositories;

public class VouchersRepository : IVouchersRepository
{

    private const string TableName = "vouchers";
    private const string PartitionKey = "voucher";
    private readonly TableClient _tableClient;

    public async Task<bool> IsVoucherAvailable(Guid voucherId, CancellationToken cancellationToken)
    {
        var voucherEntity = await _tableClient.GetEntityAsync<VoucherEntity>(
            PartitionKey,
            voucherId.ToString(),
            cancellationToken: cancellationToken);
        return voucherEntity != null && !voucherEntity.Value.PlayerId.HasValue;
    }

    public async Task<bool> ClaimVoucher(Guid voucherId, Guid playerId, CancellationToken cancellationToken)
    {
        var voucherEntity = await _tableClient.GetEntityAsync<VoucherEntity>(
            PartitionKey, 
            voucherId.ToString(), 
            cancellationToken: cancellationToken);

        if (voucherEntity.HasValue)
        {
            var entity = voucherEntity.Value;
            if (!entity.PlayerId.HasValue)
            {
                entity.PlayerId = playerId;
                var updateResponse = await _tableClient.UpdateEntityAsync(
                    entity,
                    entity.ETag,
                    TableUpdateMode.Replace,
                    cancellationToken);
                return updateResponse.Status == 204;
            }
        }

        return false;
    }

    public VouchersRepository(IOptions<AzureServices> configuration)
    {
        var tableStorageUrl = $"https://{configuration.Value.VouchersStorageAccountName}.table.core.windows.net";
        _tableClient = new TableClient(new Uri(tableStorageUrl), TableName, CloudIdentity.GetCloudIdentity());
    }
}