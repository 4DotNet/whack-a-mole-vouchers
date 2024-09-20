using Wam.Core.ErrorCodes;

namespace Wam.Vouchers.ErrorCodes;

public abstract class VouchersErrorCode : WamErrorCode
{
    public static VouchersErrorCode VoucherNotFound => new VoucherNotFound();

    public override string Namespace => $"{base.Namespace}.Vouchers";
}

public class VoucherNotFound : VouchersErrorCode
{
    public override string Code => "VoucherNotFound";
}