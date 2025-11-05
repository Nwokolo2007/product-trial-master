using AltenShop.Application.Common.Exceptions;
using AltenShop.Application.Features.Catalog.Commands;
using AltenShop.Application.Ports.Repositories;
using MediatR;

public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
{
	private readonly IProductRepository _repo;

	public UpdateProductCommandHandler(IProductRepository repo) => _repo = repo;

	public async Task<Unit> Handle(UpdateProductCommand r, CancellationToken ct)
	{
		var product = await _repo.GetByIdAsync(r.Id, ct)
			?? throw new NotFoundException("Product", r.Id);

		product.UpdateDetails(r.Name, r.Description, r.Category, r.ImageUrl);
		product.UpdatePrice(r.Price);
		product.UpdateQuantity(r.Quantity);
		

		await _repo.UpdateAsync(product, ct);
		return Unit.Value;
	}
}
