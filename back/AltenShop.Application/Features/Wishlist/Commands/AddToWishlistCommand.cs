using MediatR;

namespace AltenShop.Application.Features.Wishlist.Commands;

public record AddToWishlistCommand(Guid CustomerId, int ProductId) : IRequest<Unit>;
