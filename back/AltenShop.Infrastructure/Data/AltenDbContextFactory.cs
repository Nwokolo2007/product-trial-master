using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace AltenShop.Infrastructure.Data
{
	public class AltenDbContextFactory : IDesignTimeDbContextFactory<AltenShopDbContext>
	{
		public AltenShopDbContext CreateDbContext(string[] args)
		{
			var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../AltenShop.API");
			var builder = new ConfigurationBuilder()
				.SetBasePath(basePath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile("appsettings.Development.json", optional: true)
				.AddEnvironmentVariables();
			var config = builder.Build();

			// Fallback if not found
			var connectionString = config.GetConnectionString("DefaultConnection") ??
				"Host=localhost;Port=5432;Database=altenshop;Username=alten;Password=T9!rZb@82sKx#LpQe6vM";

			var optionsBuilder = new DbContextOptionsBuilder<AltenShopDbContext>();
			optionsBuilder.UseNpgsql(connectionString);

			return new AltenShopDbContext(optionsBuilder.Options);
		}
	}
}
