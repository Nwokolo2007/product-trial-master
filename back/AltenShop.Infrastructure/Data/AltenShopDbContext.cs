using AltenShop.Domain.Entities.Commerce;
using AltenShop.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace AltenShop.Infrastructure.Data
{
	public class AltenShopDbContext : DbContext
	{
		public AltenShopDbContext(DbContextOptions<AltenShopDbContext> options) : base(options) { }

		public DbSet<Product> Products => Set<Product>();
		public DbSet<User> Users => Set<User>();
		public DbSet<Customer> Customers => Set<Customer>();
		public DbSet<Cart> Carts => Set<Cart>();
		public DbSet<Wishlist> Wishlists => Set<Wishlist>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(AltenShopDbContext).Assembly);
		}
	}
}
