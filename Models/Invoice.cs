using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderApi.Models
{
    public class Invoice : BaseEntity
    {
        public int OrderId { get; set; }

        [Required]
        public Order Order { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }
        public int Date { get; set; }

    }
}
