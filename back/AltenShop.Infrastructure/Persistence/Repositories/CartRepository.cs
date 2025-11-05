using AltenShop.Application.Ports.Repositories;
using AltenShop.Domain.Entities.Commerce;
using AltenShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AltenShop.Infrastructure.Persistence.Repositories;

public class CartRepository : ICartRepository
{
	private readonly AltenShopDbContext _context;
	public CartRepository(AltenShopDbContext context) => _context = context;

	public async Task<Cart?> GetByCustomerIdAsync(Guid customerId, CancellationToken ct)
		=> await _context.Carts.Include(c => c.Items)
							   .FirstOrDefaultAsync(c => c.CustomerId == customerId, ct);

	public async Task AddAsync(Cart cart, CancellationToken ct)
	{
		_context.Carts.Add(cart);
		await _context.SaveChangesAsync(ct);
	}

	public async Task UpdateAsync(Cart cart, CancellationToken ct)
	{
		_context.Carts.Update(cart);
		await _context.SaveChangesAsync(ct);
	}

	public async Task ClearAsync(Guid cartId, CancellationToken ct)
	{
		var cart = await _context.Carts.FindAsync(new object[] { cartId }, ct);
		if (cart is null) return;

		_context.Carts.Remove(cart);
		await _context.SaveChangesAsync(ct);
	}
}
