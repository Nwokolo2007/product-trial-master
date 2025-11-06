using AltenShop.Application.Ports.Repositories;
using AltenShop.Domain.Entities.Commerce;
using MediatR;

namespace AltenShop.Application.Features.CustomerWishList.Commands;

public sealed class AddToWishlistCommandHandler : IRequestHandler<AddToWishlistCommand, Unit>
{
	private readonly IWishlistRepository _wishlists;
	public AddToWishlistCommandHandler(IWishlistRepository wishlists) => _wishlists = wishlists;

	public async Task<Unit> Handle(AddToWishlistCommand r, CancellationToken ct)
	{
		var wl = await _wishlists.GetByCustomerIdAsync(r.CustomerId, ct) ?? new AltenShop.Domain.Entities.Commerce.Wishlist(r.CustomerId);
		wl.AddProduct(r.ProductId);
		 await _wishlists.AddAsync(wl, ct);
		
		return Unit.Value;
	}
}
