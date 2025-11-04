using AltenShop.Application.Ports.Repositories;
using AltenShop.Domain.Entities.Identity;
using AltenShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AltenShop.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
	private readonly AltenShopDbContext _context;
	public UserRepository(AltenShopDbContext context) => _context = context;

	public async Task<User?> GetByEmailAsync(string email, CancellationToken ct)
		=> await _context.Users.FirstOrDefaultAsync(u => u.Email.Value == email, ct);

	public async Task AddAsync(User user, CancellationToken ct)
	{
		_context.Users.Add(user);
		await _context.SaveChangesAsync(ct);
	}
}
