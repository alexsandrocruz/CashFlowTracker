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
        public DateTime Date { get; set; }

        // Outros campos necessários para a consolidação podem ser adicionados aqui

        public StartConsolidationCommand(DateTime date)
        {
            Date = date;
        }
    }
}
