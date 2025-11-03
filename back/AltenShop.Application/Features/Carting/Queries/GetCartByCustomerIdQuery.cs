using AltenShop.Application.Features.Carting.DTOs;
using MediatR;

namespace AltenShop.Application.Features.Carting.Queries;

public record GetCartByCustomerIdQuery(Guid CustomerId) : IRequest<CartDto>;
