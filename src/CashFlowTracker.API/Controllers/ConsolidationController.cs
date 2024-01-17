using CashFlowTracker.Application.Commands;
using CashFlowTracker.Application.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CashFlowTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsolidationController : ControllerBase
    {
        private readonly IMediator _mediatr;

        [HttpGet("{date}")]
        public async Task<IActionResult> GetConsolidationReport(DateTime date)
        {
            return Ok(date);
        }

        [HttpPost("start-consolidation")]
        public async Task<IActionResult> StartConsolidation([FromBody] StartConsolidationCommand command)
        {
            var result = await _mediatr.Send(command);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }

    }
}
