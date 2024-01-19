using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashFlowTracker.Domain.Entities
{
    public class DailyBalance : EntityBase
    {
        public Guid AccountId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ClosingBalance { get; set; }
    }
}
