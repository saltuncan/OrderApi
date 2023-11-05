using Microsoft.EntityFrameworkCore;
using OrderApi.Models;
using OrderApi.Services;

namespace OrderApi.Jobs
{
    public class InvoiceBackgroundTask
    {
        private Task? _timerTask;
        private readonly PeriodicTimer _timer;
        private readonly CancellationTokenSource _cts = new();
        private readonly IServiceProvider _serviceProvider;

        public InvoiceBackgroundTask(TimeSpan interval, IServiceProvider serviceProvider)
        {
            _timer = new PeriodicTimer(interval);
            _serviceProvider = serviceProvider;
        }

        public void Start()
        {
            _timerTask = SendInvoices();
        }

        private async Task SendInvoices()
        {
            while (await _timer.WaitForNextTickAsync(_cts.Token))
            {
                using var scope = _serviceProvider.CreateScope();

                var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
                var orders = orderService.GetLastOrders(50);

                var mailService = scope.ServiceProvider.GetRequiredService<IMailService>();
                foreach(Order order in orders)
                {
                    try
                    {
                        _ = mailService.SendInvoice(order.Id, order.User.Email);
                        _ = orderService.UpdateOrderInvoiceSent(order);
                    }
                    catch { }
                }
            }
        }
    }
}
