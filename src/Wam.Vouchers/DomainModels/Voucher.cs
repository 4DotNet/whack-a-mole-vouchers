using HexMaster.DomainDrivenDesign;
using HexMaster.DomainDrivenDesign.ChangeTracking;

namespace Wam.Vouchers.DomainModels;

public class Voucher : DomainModel<Guid>
{

    public Guid? PlayerId { get; private set; }


    public bool Claim(Guid? playerId)
    {
        if (!PlayerId.HasValue)
        {
            PlayerId = playerId;
            SetState(TrackingState.Modified);
            return true;
        }

        return false;
    }

public Voucher(Guid id, Guid? playerId) : base(id)
    {
        PlayerId = playerId;
    }

    private Voucher() : base(Guid.NewGuid(), TrackingState.New)
    {}

    public static Voucher Create() => new Voucher();
}