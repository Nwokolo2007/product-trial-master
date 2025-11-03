using AltenShop.Application.Common.Exceptions;
using AltenShop.Application.Ports.Repositories;
using AltenShop.Application.Ports.Services;
using MediatR;

namespace AltenShop.Application.Features.Accounts.Queries;

public sealed class AuthenticateUserQueryHandler : IRequestHandler<AuthenticateUserQuery, string>
{
	private readonly IUserRepository _users;
	private readonly IJwtTokenService _jwt;
	public AuthenticateUserQueryHandler(IUserRepository users, IJwtTokenService jwt)
	{ _users = users; _jwt = jwt; }

	public async Task<string> Handle(AuthenticateUserQuery r, CancellationToken ct)
	{
		var user = await _users.GetByEmailAsync(r.Email.ToLowerInvariant(), ct)
			?? throw new NotFoundException("User", r.Email);

		if (!user.VerifyPassword(r.Password))
			throw new ConflictException("Invalid credentials.");

		return _jwt.GenerateToken(user);
	}
}
