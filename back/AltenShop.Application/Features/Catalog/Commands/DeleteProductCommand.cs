using MediatR;

namespace AltenShop.Application.Features.Catalog.Commands
{
	public record DeleteProductCommand(int Id) : IRequest<Unit>;
}
