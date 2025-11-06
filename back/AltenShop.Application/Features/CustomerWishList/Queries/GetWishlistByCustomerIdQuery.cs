using AltenShop.Application.Features.Wishlist.DTOs;
using MediatR;

namespace AltenShop.Application.Features.Wishlist.Queries;

public record GetWishlistByCustomerIdQuery(Guid CustomerId) : IRequest<WishlistDto>;
