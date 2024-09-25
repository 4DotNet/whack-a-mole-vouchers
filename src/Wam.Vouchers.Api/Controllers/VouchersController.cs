using Microsoft.AspNetCore.Mvc;
using Wam.Vouchers.Services;

namespace Wam.Vouchers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VouchersController(IVouchersService service) : ControllerBase
    {

        

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var voucherInfo = await service.IsVoucherAvailable(id, HttpContext.RequestAborted);
            return Ok(voucherInfo);
        }

        [HttpGet("{id:guid}/claim/{playerId:guid}")]
        public async Task<IActionResult> Claim(Guid id, Guid playerId)
        {
            var voucherClaimed = await service.ClaimVoucher(id, playerId, HttpContext.RequestAborted);
            return voucherClaimed ? Ok() : BadRequest();
        }

    }
}
