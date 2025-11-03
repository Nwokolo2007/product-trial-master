using AltenShop.Domain.Entities.Commerce;

namespace AltenShop.Application.Ports.Repositories
{
	public interface ICartRepository
	{
		Task<Cart?> GetByCustomerIdAsync(Guid customerId, CancellationToken ct);
		Task AddAsync(Cart cart, CancellationToken ct);
		Task UpdateAsync(Cart cart, CancellationToken ct);
	}
}
