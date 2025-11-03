using MediatR;

namespace AltenShop.Application.Features.Carting.Commands;

public record RemoveFromCartCommand(Guid CustomerId, int ProductId) : IRequest<Unit>;
