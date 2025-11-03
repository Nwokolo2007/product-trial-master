using AltenShop.Domain.Entities.Commerce;

namespace AltenShop.Application.Ports.Repositories
{
	public interface IWishlistRepository
	{
		Task<Wishlist?> GetByCustomerIdAsync(Guid customerId, CancellationToken ct);
		Task AddAsync(Wishlist wishlist, CancellationToken ct);
		Task UpdateAsync(Wishlist wishlist, CancellationToken ct);
	}
}
