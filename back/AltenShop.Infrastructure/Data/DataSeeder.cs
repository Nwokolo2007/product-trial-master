using AltenShop.Domain.Entities.Identity;
using AltenShop.Domain.ValueObjects;
using AltenShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AltenShop.Infrastructure.Data;

public static class DataSeeder
{
	public static async Task SeedAsync(AltenShopDbContext db)
	{
		// Ensure the database is created and migrations are applied
		await db.Database.MigrateAsync();

		// Seed an admin user if none exists
		if (!await db.Set<User>().AnyAsync())
		{
			var admin = new User(
				username: "admin",
				email: new EmailAddress("admin@admin.com"),
				password: PasswordHash.Create("Admin@123"),
				fullName: new FullName("System", "Administrator"),
				isAdmin: true
			);

			db.Set<User>().Add(admin);
			await db.SaveChangesAsync();
		}
	}
}
