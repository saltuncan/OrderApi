using OrderApi.Dtos;
using OrderApi.Models;
using Microsoft.EntityFrameworkCore;


namespace OrderApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderDbContext _context;
        private readonly IMailService _mailService;

        public OrderService(OrderDbContext context, IMailService mailService)
        {
            _context = context;
            _mailService = mailService;
        }
        public async Task<Order> CreateOrder(OrderCreateDto model)
        {
            Order order = new Order();
            try
            {
                order = new Order(model.UserId);
                AddOrderProducts(order, model.Products);
                await _context.Orders.AddAsync(order);
                _context.SaveChanges();
                
                SendOrderCreatedEmail(order.Id);
                return order;
            }
            catch
            {
                if (order != null)
                    _context.Remove(order);
                throw;
            }
        }

        public List<Order> GetLastOrders(int count)
        { // faturası gönderilmemiş siparişleri listeliyor
            var orders = _context.Orders.Where(x => !x.IsInvoiceSent)
                           .Include(i => i.User)
                           .OrderBy(o => o.Id)
                           .Take(count)
                           .ToList();
            return orders;
        }

        public Task UpdateOrderInvoiceSent(Order order)
        {
            order.IsInvoiceSent = true;
            _context.Update(order);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        private void SendOrderCreatedEmail(int orderId)
        {
            var order = _context.Orders.Where(x => x.Id == orderId)
                                         .Include(i => i.User)
                                         .Include(i=> i.OrderProducts).ThenInclude(i=> i.Product)
                                         .FirstOrDefault();


            SendOrderCreatedEmailDto sendOrderCreatedEmailDto = new SendOrderCreatedEmailDto();
            if (order != null)
            {
                sendOrderCreatedEmailDto.Email = order.User.Email;
                sendOrderCreatedEmailDto.Products = new List<OrderProductListItemDto>();

                foreach (OrderProducts orderProduct in order.OrderProducts)
                {
                    OrderProductListItemDto item = new OrderProductListItemDto();
                    item.ProductName = orderProduct.Product.Name;
                    item.ProductCount = orderProduct.ProductCount;
                    item.UnitPrice = orderProduct.Product.Price;
                    sendOrderCreatedEmailDto.Products.Add(item);
                }
            }

            _mailService.SendOrderCreatedInfo(sendOrderCreatedEmailDto);
        }

        private void AddOrderProducts(Order order, List<OrderProductDto> products)
        {
            try
            {
                order.OrderProducts = new List<OrderProducts>();
                foreach (OrderProductDto productDto in products)
                {
                    order.OrderProducts.Add(new OrderProducts(productDto.ProductId, productDto.ProductCount));
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

    }
}
