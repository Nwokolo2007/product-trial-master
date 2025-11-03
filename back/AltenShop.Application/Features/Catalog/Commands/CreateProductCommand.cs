using MediatR;

namespace AltenShop.Application.Features.Catalog.Commands
{
	public record CreateProductCommand(
	string Code,
	string Name,
	string Description,
	string ImageUrl,
	string Category,
	decimal Price,
	int Quantity,
	string InternalReference,
	int ShellId
) : IRequest<int>;
}
