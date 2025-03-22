using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tax.DataTransferring.TaxReturns;
using Tax.Entities.TaxReturns;
using Tax.Services.Implementations.Common;
using Tax.Services.TaxReturns.Commands;

namespace TaxReturnAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        private readonly IMapper _dtoMapper;

        public TaxController(IMapper dtoMapper)
        {
            _dtoMapper = dtoMapper;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessTaxInfoAsync(
            [FromQuery] TaxReturnInfoWriteDto taxReturnInfoWriteDto,
            [FromServices] ICommandHandler<ProcessTaxReturnInfoCommand, TaxReturnInfo> processTaxInfoCommandHandler)
        {
            TaxReturnInfo taxReturnInfo = _dtoMapper.Map<TaxReturnInfo>(taxReturnInfoWriteDto);
            ProcessTaxReturnInfoCommand command = new ProcessTaxReturnInfoCommand
            {
                TaxReturnInfo = taxReturnInfo
            };
            TaxReturnInfo processedTaxReturnInfo = await processTaxInfoCommandHandler.HandleAsync(command);
            return Ok(_dtoMapper.Map<TaxReturnInfoReadDto>(processedTaxReturnInfo));
        }
    }
}
