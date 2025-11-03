using AltenShop.Application.Ports.Repositories;
using AltenShop.Domain.Entities.Commerce;
using MediatR;

namespace AltenShop.Application.Features.Catalog.Commands
{
	public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
	{
		private readonly IProductRepository _repo;
		public CreateProductCommandHandler(IProductRepository repo) => _repo = repo;

		public async Task<int> Handle(CreateProductCommand r, CancellationToken ct)
		{
			var product = Product.Create(
			code: r.Code,
			name: r.Name,
			category: r.Category,
			price: r.Price,
			quantity: r.Quantity,
			description: r.Description,
			imageUrl: r.ImageUrl,
			internalReference: r.InternalReference,
			shellId: r.ShellId
		);


			await _repo.AddAsync(product, ct);
			return product.Id;
		}
	}
}
