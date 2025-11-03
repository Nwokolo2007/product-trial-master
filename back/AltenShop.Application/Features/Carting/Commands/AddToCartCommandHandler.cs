using AltenShop.Application.Ports.Repositories;
using AltenShop.Domain.Entities.Commerce;
using MediatR;

namespace AltenShop.Application.Features.Carting.Commands;

public sealed class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, Unit>
{
	private readonly ICartRepository _carts;
	public AddToCartCommandHandler(ICartRepository carts) => _carts = carts;

	public async Task<Unit> Handle(AddToCartCommand r, CancellationToken ct)
	{
		var cart = await _carts.GetByCustomerIdAsync(r.CustomerId, ct) ?? new Cart(r.CustomerId);
		cart.AddItem(r.ProductId, r.Quantity, r.UnitPrice);
		if (cart.Id == Guid.Empty) await _carts.AddAsync(cart, ct);
		else await _carts.UpdateAsync(cart, ct);
		return Unit.Value;
	}
}
