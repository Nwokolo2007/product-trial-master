using AltenShop.Domain.Entities.Identity;

namespace AltenShop.Application.Ports.Repositories
{
	public interface IUserRepository
	{
		Task<User?> GetByEmailAsync(string email, CancellationToken ct);
		Task AddAsync(User user, CancellationToken ct);
	}
}
