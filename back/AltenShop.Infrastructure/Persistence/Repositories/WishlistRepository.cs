using AltenShop.Application.Ports.Repositories;
using AltenShop.Domain.Entities.Commerce;
using AltenShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AltenShop.Infrastructure.Persistence.Repositories
{
	public class WishlistRepository : IWishlistRepository
	{
		private readonly AltenShopDbContext _db;

		public WishlistRepository(AltenShopDbContext db)
		{
			_db = db;
		}

		public async Task<Wishlist?> GetByCustomerIdAsync(Guid customerId, CancellationToken ct)
		{
			return await _db.Wishlists
				.Include(w => w.Items)
				.FirstOrDefaultAsync(w => w.CustomerId == customerId, ct);
		}

		public async Task AddAsync(Wishlist wishlist, CancellationToken ct)
		{
			await _db.Wishlists.AddAsync(wishlist, ct);
			await _db.SaveChangesAsync(ct);
		}

		public async Task UpdateAsync(Wishlist wishlist, CancellationToken ct)
		{
			_db.Wishlists.Update(wishlist);
			await _db.SaveChangesAsync(ct);
		}
	}
}
