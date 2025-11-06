using MediatR;

namespace AltenShop.Application.Features.CustomerWishList.Commands;

public record RemoveFromWishlistCommand(Guid CustomerId, int ProductId) : IRequest<Unit>;
