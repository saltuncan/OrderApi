using System.ComponentModel.DataAnnotations;

namespace OrderApi.Models
{
    public class OrderProducts : BaseEntity 
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        public Product Product { get; set; }
        [Required]
        public int ProductCount { get; set; }

        public OrderProducts(int productId, int productCount)
        {
            ProductId = productId;
            ProductCount = productCount;
        }
    }
}
