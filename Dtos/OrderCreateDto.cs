using Microsoft.AspNetCore.SignalR;
using Microsoft.Identity.Client;

namespace OrderApi.Dtos
{
    public class OrderCreateDto
    {
        public required List<OrderProductDto> Products { get; set; }
        public required int UserId { get; set; }
    }
}
