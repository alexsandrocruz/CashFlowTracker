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
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/Transaction
        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // GET: api/Transaction/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransaction(Guid id)
        {
            var query = new GetTransactionCommand { Id = id };
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound();
        }

        // PUT: api/Transaction/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(Guid id, [FromBody] UpdateTransactionCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(command);
            return result.Success ? Ok(result) : NotFound();
        }

        // DELETE: api/Transaction/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(Guid id)
        {
            var command = new DeleteTransactionCommand { Id = id };
            var result = await _mediator.Send(command);
            return result.Success ? Ok(result) : NotFound();
        }
    }
}
