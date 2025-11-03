using AltenShop.Application.Common.Exceptions;
using AltenShop.Application.Ports.Repositories;
using MediatR;

namespace AltenShop.Application.Features.Catalog.Commands;

public sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
{
	private readonly IProductRepository _repo;
	public DeleteProductCommandHandler(IProductRepository repo) => _repo = repo;

	public async Task<Unit> Handle(DeleteProductCommand r, CancellationToken ct)
	{
		var product = await _repo.GetByIdAsync(r.Id, ct)
			?? throw new NotFoundException("Product", r.Id);

		await _repo.DeleteAsync(product, ct);
		return Unit.Value;
	}
}
