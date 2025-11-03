using AltenShop.Application.Common.Exceptions;
using AltenShop.Application.Ports.Repositories;
using AltenShop.Domain.Entities.Identity;
using AltenShop.Domain.ValueObjects;
using MediatR;

namespace AltenShop.Application.Features.Accounts.Commands;

public sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
{
	private readonly IUserRepository _users;
	public RegisterUserCommandHandler(IUserRepository users) => _users = users;

	public async Task<Guid> Handle(RegisterUserCommand r, CancellationToken ct)
	{
		var existing = await _users.GetByEmailAsync(r.Email.ToLowerInvariant(), ct);
		if (existing is not null) throw new ConflictException("Email already in use.");

		var user = new User(
			r.Username,
			new EmailAddress(r.Email),
			PasswordHash.Create(r.Password),
			new FullName(r.FirstName, r.LastName));

		await _users.AddAsync(user, ct);
		return user.Id;
	}
}
