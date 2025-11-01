using Microsoft.EntityFrameworkCore;

namespace AltenShop.Infrastructure.Data
{
	public class AltenShopDbContext : DbContext
	{
		public AltenShopDbContext(DbContextOptions<AltenShopDbContext> options) : base(options) { }
	}
}
