using HexMaster.DomainDrivenDesign;
using HexMaster.DomainDrivenDesign.ChangeTracking;

namespace Wam.Vouchers.DomainModels;

public class Voucher : DomainModel<Guid>
{
    private Voucher() : base(Guid.NewGuid(), TrackingState.New)
    {}

    public static Voucher Create() => new Voucher();
}