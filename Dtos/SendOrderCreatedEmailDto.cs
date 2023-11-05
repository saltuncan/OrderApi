using OrderApi.Models;

namespace OrderApi.Dtos
{
    public class SendOrderCreatedEmailDto
    {
        public string Email { get; set; }

        public List<OrderProductListItemDto> Products { get; set; }
    }
}
