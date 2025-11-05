using AltenShop.Domain.Exceptions;
using System.Security.Cryptography;
using System.Text;

namespace AltenShop.Domain.ValueObjects
{
	public sealed class PasswordHash
	{
		private const int SaltSize = 16;
		private const int KeySize = 32;
		private const int Iterations = 120_000;
		private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

		public string Hash { get; }
		public string Salt { get; }
		public string Version { get; } = "PBKDF2-SHA512-v2";
		private PasswordHash() { }
		private PasswordHash(string hash, string salt)
		{
			Hash = hash;
			Salt = salt;
		}

		/// <summary>
		/// Create a new hash from plain text password
		/// </summary>

		public static PasswordHash Create(string plainPassword)
		{
			if (string.IsNullOrWhiteSpace(plainPassword))
				throw new DomainException("Password is required.");
			if (plainPassword.Length < 8)
				throw new DomainException("Password must be at least 8 characters.");

			var saltBytes = RandomNumberGenerator.GetBytes(SaltSize);

			var hashBytes = Rfc2898DeriveBytes.Pbkdf2(
				Encoding.UTF8.GetBytes(plainPassword),
				saltBytes,
				Iterations,
				Algorithm,
				KeySize);

			var hash = Convert.ToBase64String(hashBytes);
			var salt = Convert.ToBase64String(saltBytes);

			return new PasswordHash(hash, salt);
		}

		/// <summary>
		/// Verifies that a plaintext password matches this hash.
		/// </summary>

		public bool Verify(string password)
		{
			var saltBytes = Convert.FromBase64String(Salt);
			var hashBytes = Convert.FromBase64String(Hash);

			var computedHash = Rfc2898DeriveBytes.Pbkdf2(
				Encoding.UTF8.GetBytes(password),
				saltBytes,
				Iterations,
				Algorithm,
				KeySize);

			return CryptographicOperations.FixedTimeEquals(hashBytes, computedHash);
		}

		/// <summary>
		/// Serializes this hash for storage (Version|Salt|Hash).
		/// </summary>
		public override string ToString() => $"{Version}|{Salt}|{Hash}";

		/// <summary>
		/// Rehydrates from stored string.
		/// </summary>

		public static PasswordHash Parse(string stored)
		{
			var parts = stored.Split('|');
			if (parts.Length != 3)
				throw new DomainException("Invalid password hash format.");
			return new PasswordHash(parts[2], parts[1]);
		}
	}
}
