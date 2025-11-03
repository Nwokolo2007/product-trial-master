using MediatR;

namespace AltenShop.Application.Features.Wishlist.Commands;

public record RemoveFromWishlistCommand(Guid CustomerId, int ProductId) : IRequest<Unit>;
