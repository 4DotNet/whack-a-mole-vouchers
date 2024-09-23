
using Wam.Vouchers.DataTransferObjects;

namespace Wam.Vouchers.Services;

public interface IVouchersService
{

    Task<VoucherAvailableResponse> IsVoucherAvailable(Guid voucherId, CancellationToken cancellationToken);
    Task<bool> ClaimVoucher(Guid voucherId, Guid playerId, CancellationToken cancellationToken);

}