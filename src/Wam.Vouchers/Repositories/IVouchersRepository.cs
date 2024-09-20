namespace Wam.Vouchers.Repositories;

public interface IVouchersRepository
{
    Task<bool> IsVoucherAvailable(Guid voucherId, CancellationToken cancellationToken);
    Task<bool> ClaimVoucher(Guid voucherId, Guid playerId, CancellationToken cancellationToken);
}