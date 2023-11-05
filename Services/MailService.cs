using System.Net;
using System.Net.Mail;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using OrderApi.Dtos;

namespace OrderApi.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task SendInvoice(int orderId, string userEmail) 
        {
            try
            {
                IConfigurationSection smtpConfig = _configuration.GetSection("EmailConfiguration");
                SmtpClient client = GetSmtpInfo(smtpConfig);

                MailMessage message = new MailMessage(smtpConfig["From"]!, userEmail);
                message.Subject = "Faturanız Oluşturuldu";
                message.Body ="Değerli Müşterimiz, "+orderId+" nolu siparişinize ait faturanız oluşturulmuştur. Ekte iletilmektedir. İyi Günler Dileriz.";
                // client.Send(message); mail gönderemediği için backgroudtaskta takıldıgı icin bu kısım kapatıldı
                return Task.CompletedTask;
            }
            catch(Exception ex)
            {
                return Task.FromException(ex);
            }
        }

        public Task SendOrderCreatedInfo(SendOrderCreatedEmailDto sendOrderCreatedEmailDto)
        {
            try
            {
                IConfigurationSection smtpConfig = _configuration.GetSection("EmailConfiguration");
                SmtpClient client = GetSmtpInfo(smtpConfig);

                MailMessage message = new MailMessage(smtpConfig["From"]!,sendOrderCreatedEmailDto.Email);
                message.Subject = "Siparişiniz Oluşturuldu";
                message.Body = CreateMailMessage(sendOrderCreatedEmailDto.Products);
                client.Send(message);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                return Task.FromException(ex);
            }
        }

        private SmtpClient GetSmtpInfo(IConfigurationSection smtpConfig)
        {
            SmtpClient client = new SmtpClient(smtpConfig["SmtpServer"], int.Parse(smtpConfig["Port"]!));
            client.UseDefaultCredentials = false;
            NetworkCredential authInfo = new NetworkCredential(smtpConfig["Username"], smtpConfig["Password"]);
            client.Credentials = authInfo;
            client.EnableSsl = true;
            return client;
        }
        private string CreateMailMessage(List<OrderProductListItemDto> products)
        {
            string messageText = "<html><body>Değerli Müşterimiz,<br>";
            messageText += "Siparişiniz oluşturulmuştur. Sipariş detayı:<br><table>";
            foreach(OrderProductListItemDto product in products)
            {
                messageText += "<tr><td>Ürün Adı</td><td>"+product.ProductName+"</td>";
                messageText += "<tr><td>Ürün Adeti</td><td>" + product.ProductCount.ToString() + "</td>";
                messageText += "<tr><td>Birim Fiyatı</td><td>" + product.UnitPrice + "</td></tr>";
            }
            messageText += "</table></html>";
            return messageText;
        }
    }
}
