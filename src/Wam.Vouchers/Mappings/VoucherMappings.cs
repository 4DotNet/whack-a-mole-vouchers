using Wam.Vouchers.DomainModels;
using Wam.Vouchers.Entities;
using Wam.Vouchers.Repositories;

namespace Wam.Vouchers.Mappings;

public static class VoucherMappings
{

    public static Voucher ToDomainModel(this VoucherEntity entity)
    {
        return new Voucher(Guid.Parse(entity.RowKey), entity.PlayerId);
    }

    public static VoucherEntity ToEntity(this Voucher domainModel, VoucherEntity? voucherEntity = null)
    {

        var entity = voucherEntity ?? new VoucherEntity
        {
            PartitionKey = VouchersRepository.PartitionKey,
            RowKey = domainModel.Id.ToString()
        };

        entity.PlayerId = domainModel.PlayerId;
        return entity;
    }

}