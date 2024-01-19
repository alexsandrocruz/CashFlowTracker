
namespace CashFlowTracker.Application.Commands
{
    public class TransactionResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Guid? TransactionId { get; set; } // ID da transação criada, se for bem-sucedida
    }

}
