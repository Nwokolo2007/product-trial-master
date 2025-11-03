using MediatR;

namespace AltenShop.Application.Features.Catalog.Commands
{
	public record UpdateProductCommand(

		int Id,
		string Name,
		string Description,
		string ImageUrl,
		string Category,
		decimal Price,
		int Quantity
	) : IRequest<Unit>;
}
