using MediatR;

namespace AltenShop.Application.Features.Carting.Commands;

public record ClearCartCommand(Guid CustomerId) : IRequest<Unit>;
