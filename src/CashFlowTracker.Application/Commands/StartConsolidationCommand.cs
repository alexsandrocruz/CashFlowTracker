using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowTracker.Application.Commands
{
    public class StartConsolidationCommand : IRequest<ConsolidationResult>
    {
        public Guid AccountId { get; set; }
        public DateTime Date { get; set; }

    }
}
