using OrderApi.Dtos;
using OrderApi.Models;

namespace OrderApi.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(OrderCreateDto model);
        List<Order> GetLastOrders(int count);
        Task UpdateOrderInvoiceSent(Order order);
    }
}
