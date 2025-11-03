using AltenShop.Domain.Entities.Identity;

namespace AltenShop.Application.Ports.Services
{
	public interface IJwtTokenService
	{
		string GenerateToken(User user);
	}
}
