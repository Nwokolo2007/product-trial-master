using MediatR;

namespace AltenShop.Application.Features.CustomerWishList.Commands;

public record AddToWishlistCommand(Guid CustomerId, int ProductId) : IRequest<Unit>;
