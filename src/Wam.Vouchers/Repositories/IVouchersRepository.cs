using Wam.Vouchers.DomainModels;

namespace Wam.Vouchers.Repositories;

public interface IVouchersRepository
{
    Task<Voucher> Get(Guid id, CancellationToken cancellationToken);
    Task<bool> Update(Voucher domainModel, CancellationToken cancellationToken);
}