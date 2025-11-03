using AltenShop.Domain.Entities.Commerce;

namespace AltenShop.Application.Ports.Repositories
{
	public interface IProductRepository
	{
		Task<(IReadOnlyList<Product> Products, int TotalCount)> GetPaginatedAsync(
			int pageNumber, int pageSize, string? category, string? search, CancellationToken ct);

		Task<Product?> GetByIdAsync(int id, CancellationToken ct);
		Task AddAsync(Product product, CancellationToken ct);
		Task UpdateAsync(Product product, CancellationToken ct);
		Task DeleteAsync(Product product, CancellationToken ct);
	}
}
