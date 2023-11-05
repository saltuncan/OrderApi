using OrderApi.Dtos;

namespace OrderApi.Services
{
    public interface IMailService
    {
        Task SendInvoice(int orderId, string userEmail);
        Task SendOrderCreatedInfo(SendOrderCreatedEmailDto sendOrderCreatedEmailDto);
    }
}
