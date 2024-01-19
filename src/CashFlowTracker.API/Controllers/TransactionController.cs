using CashFlowTracker.Application.Commands;
using CashFlowTracker.Application.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CashFlowTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediatr;
        public TransactionController(IMediator mediator)
        {
            _mediatr = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionCommand command)
        {
            var result = await _mediatr.Send(command);

            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }        
    }
}
