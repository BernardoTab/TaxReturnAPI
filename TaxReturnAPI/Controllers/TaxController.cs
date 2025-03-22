using Microsoft.AspNetCore.Mvc;
using Tax.DataTransferring.TaxReturns;

namespace TaxReturnAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> ProcessTaxInfoAsync()
        {
            TaxReturnReadDto taxReturnInfo = new TaxReturnReadDto
            {
                GrossValue = 12,
                NetValue = 24,
                VATValue = 15
            };
            return Ok(taxReturnInfo);
        }
    }
}
