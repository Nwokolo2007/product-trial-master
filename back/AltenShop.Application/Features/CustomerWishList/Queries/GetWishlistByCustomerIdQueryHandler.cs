using AltenShop.Application.Common.Exceptions;
using AltenShop.Application.Features.Wishlist.DTOs;
using AltenShop.Application.Ports.Repositories;
using MediatR;

namespace AltenShop.Application.Features.Wishlist.Queries;

public sealed class GetWishlistByCustomerIdQueryHandler : IRequestHandler<GetWishlistByCustomerIdQuery, WishlistDto>
{
	private readonly IWishlistRepository _wishlists;
	public GetWishlistByCustomerIdQueryHandler(IWishlistRepository wishlists) => _wishlists = wishlists;

	public async Task<WishlistDto> Handle(GetWishlistByCustomerIdQuery r, CancellationToken ct)
	{
		var wl = await _wishlists.GetByCustomerIdAsync(r.CustomerId, ct)
			?? throw new NotFoundException("Wishlist", r.CustomerId);

		var items = wl.Items.Select(i => new WishlistItemDto(i.ProductId)).ToList();
		return new WishlistDto(wl.Id, wl.CustomerId, items);
	}
}
