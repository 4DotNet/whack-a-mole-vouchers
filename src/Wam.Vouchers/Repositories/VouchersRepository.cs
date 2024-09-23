using Azure;
using Azure.Data.Tables;
using HexMaster.DomainDrivenDesign.ChangeTracking;
using Microsoft.Extensions.Options;
using Wam.Core.Configuration;
using Wam.Core.Identity;
using Wam.Vouchers.DomainModels;
using Wam.Vouchers.Entities;
using Wam.Vouchers.ErrorCodes;
using Wam.Vouchers.Exceptions;
using Wam.Vouchers.Mappings;

namespace Wam.Vouchers.Repositories;

public class VouchersRepository : IVouchersRepository
{

    private const string TableName = "vouchers";
    public const string PartitionKey = "Voucher";
    private readonly TableClient _tableClient;

    public async Task<bool> Update(Voucher domainModel, CancellationToken cancellationToken)
    {
        if (domainModel.TrackingState == TrackingState.Modified)
        {
            var voucherEntity = await _tableClient.GetEntityAsync<VoucherEntity>(
                PartitionKey,
                domainModel.Id.ToString(),
                cancellationToken: cancellationToken);

            var entity = domainModel.ToEntity(voucherEntity);
            var updateResponse = await _tableClient.UpdateEntityAsync(
                entity,
                entity.ETag,
                TableUpdateMode.Replace,
                cancellationToken);


            return !updateResponse.IsError;
        }

        return false;
    }

    public async Task<Voucher> Get(Guid id, CancellationToken cancellationToken)
        {
            var voucherEntity = await _tableClient.GetEntityAsync<VoucherEntity>(
                PartitionKey,
                id.ToString(),
                cancellationToken: cancellationToken);

            if (voucherEntity.HasValue)
            {
                return voucherEntity.Value.ToDomainModel();
            }

            throw new WamVouchersException(VouchersErrorCode.VoucherNotFound,
                $"The voucher with ID '{id}' could not be found");
        }





    public VouchersRepository(IOptions<AzureServices> configuration)
    {
        var tableStorageUrl = $"https://{configuration.Value.VouchersStorageAccountName}.table.core.windows.net";
        _tableClient = new TableClient(new Uri(tableStorageUrl), TableName, CloudIdentity.GetCloudIdentity());
    }
}