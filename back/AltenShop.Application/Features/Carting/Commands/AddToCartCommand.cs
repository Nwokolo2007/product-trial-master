using MediatR;

namespace AltenShop.Application.Features.Carting.Commands;

public record AddToCartCommand(Guid CustomerId, int ProductId, int Quantity, decimal UnitPrice) : IRequest<Unit>;
