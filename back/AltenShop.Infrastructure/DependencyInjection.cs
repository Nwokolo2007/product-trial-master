using AltenShop.Application.Ports.Repositories;
using AltenShop.Application.Ports.Services;
using AltenShop.Infrastructure.Data;
using AltenShop.Infrastructure.Persistence.Repositories;
using AltenShop.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AltenShop.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
	{
		services.AddDbContext<AltenShopDbContext>(options =>
			options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

		// Repositories
		services.AddScoped<IProductRepository, ProductRepository>();
		services.AddScoped<IUserRepository, UserRepository>();
		services.AddScoped<ICartRepository, CartRepository>();
		services.AddScoped<IWishlistRepository,WishlistRepository>();

		// JWT
		services.AddScoped<IJwtTokenService, JwtTokenService>();

		return services;
	}

	public static async Task InitializeDatabaseAsync(this IServiceProvider services)
	{
		using var scope = services.CreateScope();
		var db = scope.ServiceProvider.GetRequiredService<AltenShopDbContext>();
		await DataSeeder.SeedAsync(db);
	}
}
