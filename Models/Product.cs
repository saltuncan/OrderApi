using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderApi.Models
{
    public class Product : BaseEntity
    {
        [Required]
        public required string Name { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        [Required]
        public decimal Price { get; set; }

    }
}
