using Microsoft.Extensions.Logging;
using Wam.Core.Abstractions;
using Wam.Core.Cache;
using Wam.Vouchers.DataTransferObjects;
using Wam.Vouchers.Repositories;

namespace Wam.Vouchers.Services;

public class VouchersService(
    IVouchersRepository repository,
    ILogger<VouchersService> logger,
    IWamCacheService wamCache) : IVouchersService
{
    public async Task<VoucherAvailableResponse> IsVoucherAvailable(Guid voucherId, CancellationToken cancellationToken)
    {
        var cacheName = CacheName.VoucherInfo(voucherId);
        return  await wamCache.GetFromCacheOrInitialize(cacheName,
            () => IsVoucherAvailableFromRepository(voucherId, cancellationToken), cancellationToken: cancellationToken);
    }
    private async Task<VoucherAvailableResponse> IsVoucherAvailableFromRepository(Guid voucherId, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching voucher information for voucher {voucherId}", voucherId);
        var voucher = await repository.Get(voucherId, cancellationToken);
        logger.LogInformation("Returning voucher information for voucher {voucherId}", voucherId);
        return new VoucherAvailableResponse(voucher.Id, !voucher.PlayerId.HasValue);

    }

    public async Task<bool> ClaimVoucher(Guid voucherId, Guid playerId, CancellationToken cancellationToken)
    {
        logger.LogInformation("Claiming voucher {voucherId} for player {playerId}", voucherId, playerId);

        var voucher = await repository.Get(voucherId, cancellationToken);
        if (voucher.Claim(playerId))
        {
            logger.LogInformation("Voucher {voucherId} successfully claimed for player {playerId}", voucherId, playerId);
            var updateResult = await repository.Update(voucher, cancellationToken);
            logger.LogInformation("Voucher {voucherId} updated in repository (success {updateResult})", voucherId, updateResult);
            await InvalidateCacheForVoucher(voucherId, cancellationToken);
            return updateResult;
        }

        return false;
    }

    private async Task InvalidateCacheForVoucher(Guid voucherId, CancellationToken cancellationToken)
    {
        try
        {
            var cacheName = CacheName.VoucherInfo(voucherId);
            await wamCache.Invalidate(cacheName, cancellationToken);
            logger.LogInformation("Cache invalidated for voucher {voucherId}", voucherId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to invalidate cache for voucher {voucherId}", voucherId);
        }
    }



}