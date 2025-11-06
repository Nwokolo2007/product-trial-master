using AltenShop.Domain.Entities.Identity;

namespace AltenShop.Application.Ports.Services
{
	public interface IJwtTokenService
	{
		Task<string> GenerateToken(User user, CancellationToken ct);
	}
}
