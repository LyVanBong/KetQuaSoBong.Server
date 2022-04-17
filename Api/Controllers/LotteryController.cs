using Api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LotteryController : ControllerBase
    {
        [HttpGet("northern/{date}")]
        public async Task<IActionResult> GetNorthern(string date)
        {
            var kq = await XoSoMienBac.GetData(@"https://xoso.me/xsmb-{0}-ket-qua-xo-so-mien-bac-ngay-{0}.html", date);
            return Ok(kq);
        }
        [HttpGet("central/{date}")]
        public async Task<IActionResult> GetCentral(string date)
        {
            var kq = await XoSoMienNam.GetData(@"https://xoso.me/xsmt-{0}-ket-qua-xo-so-mien-trung-ngay-{0}.html", date);
            return Ok(kq);

        }
        [HttpGet("south/{date}")]
        public async Task<IActionResult> GetSouth(string date)
        {
            var kq = await XoSoMienNam.GetData(@"https://xoso.me/xsmn-{0}-ket-qua-xo-so-mien-nam-ngay-{0}.html", date);
            return Ok(kq);
        }
    }
}
