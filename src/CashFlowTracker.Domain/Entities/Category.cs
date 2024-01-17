using System.ComponentModel.DataAnnotations;

namespace CashFlowTracker.Domain.Entities
{
    public class Category : EntityBase
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
