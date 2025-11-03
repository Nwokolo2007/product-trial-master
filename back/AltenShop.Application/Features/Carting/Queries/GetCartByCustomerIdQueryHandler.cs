using AltenShop.Application.Common.Exceptions;
using AltenShop.Application.Features.Carting.DTOs;
using AltenShop.Application.Ports.Repositories;
using MediatR;

namespace AltenShop.Application.Features.Carting.Queries;

public sealed class GetCartByCustomerIdQueryHandler : IRequestHandler<GetCartByCustomerIdQuery, CartDto>
{
	private readonly ICartRepository _carts;
	public GetCartByCustomerIdQueryHandler(ICartRepository carts) => _carts = carts;

	public async Task<CartDto> Handle(GetCartByCustomerIdQuery r, CancellationToken ct)
	{
		var cart = await _carts.GetByCustomerIdAsync(r.CustomerId, ct)
			?? throw new NotFoundException("Cart", r.CustomerId);

		var items = cart.Items
			.Select(i => new CartItemDto(i.ProductId, i.Quantity, i.UnitPrice, i.SubTotal))
			.ToList();

		return new CartDto(cart.Id, cart.CustomerId, items, cart.Total);
	}
}
