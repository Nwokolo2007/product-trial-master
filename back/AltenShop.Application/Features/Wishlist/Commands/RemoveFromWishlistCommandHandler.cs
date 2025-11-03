using AltenShop.Application.Common.Exceptions;
using AltenShop.Application.Ports.Repositories;
using MediatR;

namespace AltenShop.Application.Features.Wishlist.Commands;

public sealed class RemoveFromWishlistCommandHandler : IRequestHandler<RemoveFromWishlistCommand, Unit>
{
	private readonly IWishlistRepository _wishlists;
	public RemoveFromWishlistCommandHandler(IWishlistRepository wishlists) => _wishlists = wishlists;

	public async Task<Unit> Handle(RemoveFromWishlistCommand r, CancellationToken ct)
	{
		var wl = await _wishlists.GetByCustomerIdAsync(r.CustomerId, ct)
			?? throw new NotFoundException("Wishlist", r.CustomerId);

		wl.RemoveProduct(r.ProductId);
		await _wishlists.UpdateAsync(wl, ct);
		return Unit.Value;
	}
}
