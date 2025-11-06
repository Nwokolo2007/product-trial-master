using AltenShop.Application.Ports.Repositories;
using AltenShop.Domain.Entities.Commerce;
using Microsoft.EntityFrameworkCore;

namespace AltenShop.Infrastructure.Repositories;

public sealed class CustomerRepository : ICustomerRepository
{
	private readonly AltenShopDbContext _db;

	public CustomerRepository(AltenShopDbContext db)
		=> _db = db;

	public async Task AddAsync(Customer customer, CancellationToken ct)
	{
		await _db.Customers.AddAsync(customer, ct);
		await _db.SaveChangesAsync(ct);
	}

	public async Task<Customer?> GetByUserIdAsync(Guid userId, CancellationToken ct)
	{
		return await _db.Customers
			.AsNoTracking()
			.FirstOrDefaultAsync(c => c.UserId == userId, ct);
	}
}
