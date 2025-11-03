using AltenShop.Application.Common.Exceptions;
using AltenShop.Application.Ports.Repositories;
using MediatR;

namespace AltenShop.Application.Features.Carting.Commands;

public sealed class RemoveFromCartCommandHandler : IRequestHandler<RemoveFromCartCommand, Unit>
{
	private readonly ICartRepository _carts;
	public RemoveFromCartCommandHandler(ICartRepository carts) => _carts = carts;

	public async Task<Unit> Handle(RemoveFromCartCommand r, CancellationToken ct)
	{
		var cart = await _carts.GetByCustomerIdAsync(r.CustomerId, ct)
			?? throw new NotFoundException("Cart", r.CustomerId);

		cart.RemoveItem(r.ProductId);
		await _carts.UpdateAsync(cart, ct);
		return Unit.Value;
	}
}
