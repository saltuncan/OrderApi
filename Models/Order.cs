namespace OrderApi.Models
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public bool IsInvoiceSent { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<OrderProducts> OrderProducts { get; set; }
        public Order()
        {

        }
        public Order(int userId)
        {
            OrderDate = DateTime.Now;
            UserId = userId;
            IsInvoiceSent = false;
        }
    }
}
