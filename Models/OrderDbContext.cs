using Microsoft.EntityFrameworkCore;

namespace OrderApi.Models
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProducts> OrderProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User() { Id = 1, UserName = "user1", Email = "user@xyz.com" }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product() { Id = 1, Name = "Product 1", Price= 1200},
                new Product() { Id = 2, Name = "Product 2", Price = 3400 },
                new Product() { Id = 3, Name = "Product 3", Price = 777 }
                );
        }
    }

}
