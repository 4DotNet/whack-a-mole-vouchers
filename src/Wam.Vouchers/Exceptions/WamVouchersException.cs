using Wam.Core.Exceptions;
using Wam.Vouchers.ErrorCodes;

namespace Wam.Vouchers.Exceptions;

public class WamVouchersException(VouchersErrorCode error, string message) : WamException(error, message);