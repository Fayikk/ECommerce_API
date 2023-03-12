using Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_API.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get;set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Basket> Baskets { get; set; }  
        public DbSet<UserMoney> UserMoneys { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
