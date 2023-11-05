using Microsoft.AspNetCore.Mvc;
using OrderApi.Dtos;
using OrderApi.Models;
using OrderApi.Services;

namespace OrderApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Route("CreateOrder")]
        public async Task<Order> CreateOrder(OrderCreateDto input)
        {
            Order order = await _orderService.CreateOrder(input);
            return order;
        }
    }
}
