using CashFlowTracker.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashFlowTracker.Domain.Entities
{
    public class Transaction : EntityBase
    {
        public Guid AccountId { get; set; }

        [Required]
        public TransactionType Type { get; set; } 

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        public string Description { get; set; }
    }
}
