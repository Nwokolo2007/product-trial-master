using MediatR;

namespace AltenShop.Application.Features.Accounts.Commands;

public record RegisterUserCommand(
	string Username,
	string FirstName,
	string LastName,
	string Email,
	string Password
) : IRequest<Guid>;
