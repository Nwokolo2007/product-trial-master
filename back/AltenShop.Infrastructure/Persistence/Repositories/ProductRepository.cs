using AltenShop.Application.Ports.Repositories;
using AltenShop.Domain.Entities.Commerce;
using AltenShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AltenShop.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
	private readonly AltenShopDbContext _context;

	public ProductRepository(AltenShopDbContext context) => _context = context;

	public async Task<(IReadOnlyList<Product>, int)> GetPaginatedAsync(
		int pageNumber, int pageSize, string? category, string? search, CancellationToken ct)
	{
		var query = _context.Products.AsQueryable();

		if (!string.IsNullOrWhiteSpace(category))
			query = query.Where(p => p.Category.ToLower() == category.ToLower());

		if (!string.IsNullOrWhiteSpace(search))
			query = query.Where(p => p.Name.ToLower().Contains(search.ToLower()));

		var total = await query.CountAsync(ct);
		var items = await query
			.OrderByDescending(p => p.CreatedAtUtc)
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync(ct);

		return (items, total);
	}

	public Task<Product?> GetByIdAsync(int id, CancellationToken ct)
		=> _context.Products.FirstOrDefaultAsync(p => p.Id == id, ct);

	public async Task AddAsync(Product product, CancellationToken ct)
	{
		_context.Products.Add(product);
		await _context.SaveChangesAsync(ct);
	}

	public async Task UpdateAsync(Product product, CancellationToken ct)
	{
		_context.Products.Update(product);
		await _context.SaveChangesAsync(ct);
	}

	public async Task DeleteAsync(Product product, CancellationToken ct)
	{
		_context.Products.Remove(product);
		await _context.SaveChangesAsync(ct);
	}
}
