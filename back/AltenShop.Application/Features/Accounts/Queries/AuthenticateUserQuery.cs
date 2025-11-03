using MediatR;

namespace AltenShop.Application.Features.Accounts.Queries;

public record AuthenticateUserQuery(string Email, string Password) : IRequest<string>; // JWT
