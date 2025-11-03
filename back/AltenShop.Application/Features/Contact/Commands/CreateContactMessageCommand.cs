using MediatR;

namespace AltenShop.Application.Features.Contact.Commands;

public record CreateContactMessageCommand(string Email, string Message) : IRequest<Guid>;
