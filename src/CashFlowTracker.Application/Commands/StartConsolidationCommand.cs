using MediatR;

namespace CashFlowTracker.Application.Commands
{
    public class StartConsolidationCommand : IRequest<ConsolidationResult>
    {
        public Guid AccountId { get; set; }
        public DateTime Date { get; set; }

    }
}
