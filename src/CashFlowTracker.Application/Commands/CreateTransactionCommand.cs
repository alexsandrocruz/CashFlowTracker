using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowTracker.Application.Commands
{
    public class CreateTransactionCommand : IRequest<TransactionResult>
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } // Exemplo: "Debit" ou "Credit"
        public DateTime Date { get; set; }
        public string Description { get; set; }

        // Construtor
        public CreateTransactionCommand(Guid accountId, decimal amount, string type, DateTime date, string description)
        {
            AccountId = accountId;
            Amount = amount;
            Type = type;
            Date = date;
            Description = description;
        }
    }
}
