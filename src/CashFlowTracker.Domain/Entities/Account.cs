using CashFlowTracker.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashFlowTracker.Domain.Entities
{
    public class Account : EntityBase
    {
        [Required]
        [MaxLength(100)]
        public string AccountNumber { get; set; }

        [Required]
        [MaxLength(200)]
        public string AccountHolderName { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentBalance { get; set; }

        [Required]
        public AccountType AccountType { get; set; } 

        public ICollection<Transaction> Transactions { get; set; }

        public ICollection<DailyBalance> DailyBalances { get; set; }
    }
}
