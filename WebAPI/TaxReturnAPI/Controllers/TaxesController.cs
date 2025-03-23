using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tax.DataTransferring.TaxReturns;
using Tax.Entities.TaxReturns;
using Tax.Services.Implementations.Common;
using Tax.Services.TaxReturns.Commands;
using TaxReturnAPI.ModelState;

namespace TaxReturnAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class TaxesController : ControllerBase
    {
        private readonly IMapper _dtoMapper;

        public TaxesController(IMapper dtoMapper)
        {
            _dtoMapper = dtoMapper;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessTaxInfoAsync(
            [FromBody] TaxReturnInfoWriteDto taxReturnInfoWriteDto,
            [FromServices] ICommandHandler<ProcessTaxReturnInfoCommand, ProcessedTaxReturnInfo> processTaxInfoCommandHandler)
        {
            if (!ModelState.IsValid)
            {
                ModelState.ThrowInvalidModelStateExceptions();
            }
            TaxReturnInfo taxReturnInfo = _dtoMapper.Map<TaxReturnInfo>(taxReturnInfoWriteDto);
            ProcessTaxReturnInfoCommand command = new ProcessTaxReturnInfoCommand
            {
                TaxReturnInfo = taxReturnInfo
            };
            ProcessedTaxReturnInfo processedTaxReturnInfo = await processTaxInfoCommandHandler.HandleAsync(command);
            return Ok(_dtoMapper.Map<TaxReturnInfoReadDto>(processedTaxReturnInfo));
        }
    }
}
