using CashFlowTracker.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CashFlowTracker.Domain.Entities
{
    public class ConsolidationLog : EntityBase
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public ConsolidationLogStatus Status { get; set; } 

        public string ErrorMessage { get; set; }
    }
}
