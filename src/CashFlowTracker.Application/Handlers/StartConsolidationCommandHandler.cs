using CashFlowTracker.Application.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CashFlowTracker.Application.Handlers
{
    public class StartConsolidationCommandHandler : IRequestHandler<StartConsolidationCommand, ConsolidationResult>
    {
        public async Task<ConsolidationResult> Handle(StartConsolidationCommand request, CancellationToken cancellationToken)
        {
            // Lógica de consolidação aqui
            // Por exemplo, consolidar transações para a data especificada

            try
            {
                // Simulando a lógica de consolidação
                // ...

                return new ConsolidationResult { Success = true, Message = "Consolidação realizada com sucesso." };
            }
            catch (Exception ex)
            {
                // Tratar exceções conforme necessário
                return new ConsolidationResult { Success = false, Message = ex.Message };
            }
        }
    }
}
