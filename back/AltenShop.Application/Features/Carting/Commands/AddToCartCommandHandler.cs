using AltenShop.Application.Ports.Repositories;
using AltenShop.Domain.Entities.Commerce;
using AltenShop.Domain.Exceptions;
using MediatR;

namespace AltenShop.Application.Features.Carting.Commands;

public sealed class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, Unit>
{
	private readonly ICartRepository _cartRepo;
	private readonly IProductRepository _productRepo;

	public AddToCartCommandHandler(ICartRepository cartRepo, IProductRepository productRepo)
	{
		_cartRepo = cartRepo;
		_productRepo = productRepo;
	}

	public async Task<Unit> Handle(AddToCartCommand request, CancellationToken ct)
	{

		if (request.Quantity <= 0)
			throw new DomainException("Quantity must be greater than zero.");


		var product = await _productRepo.GetByIdAsync(request.ProductId, ct)
			?? throw new DomainException($"Product with ID {request.ProductId} not found.");


		var cart = await _cartRepo.GetByCustomerIdAsync(request.CustomerId, ct)
			?? new Cart(request.CustomerId);


		cart.AddItem(product.Id, request.Quantity, product.Price);

		await _cartRepo.AddAsync(cart, ct);

		return Unit.Value;
	}
}
