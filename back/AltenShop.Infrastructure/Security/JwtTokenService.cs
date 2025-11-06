using AltenShop.Application.Ports.Repositories;
using AltenShop.Application.Ports.Services;
using AltenShop.Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AltenShop.Infrastructure.Security
{
	public sealed class JwtTokenService : IJwtTokenService
	{
		private readonly IConfiguration _config;
		private readonly ICustomerRepository _customerRepo;

		public JwtTokenService(IConfiguration config, ICustomerRepository customerRepo)
		{
			_config = config;
			_customerRepo =  customerRepo;
		}

		public async Task<string> GenerateToken(User user, CancellationToken ct)
		{
			var jwtKey = _config["Jwt:Key"] ?? throw new InvalidOperationException("JWT key not configured.");
			var jwtIssuer = _config["Jwt:Issuer"] ?? "alten.shop";
			var customer = await _customerRepo.GetByUserIdAsync(user.Id, ct);
			var customerId = customer?.Id.ToString() ?? string.Empty;
			var claims = new List<Claim>
			{
				new(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new(ClaimTypes.Email, user.Email.Value),
				 new("customerId", customerId),
			};

			if (user.IsAdmin)
				claims.Add(new(ClaimTypes.Role, "Admin"));

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		

			var token = new JwtSecurityToken(
				issuer: jwtIssuer,
				audience: jwtIssuer,
				claims: claims,

				expires: DateTime.UtcNow.AddHours(8),
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
