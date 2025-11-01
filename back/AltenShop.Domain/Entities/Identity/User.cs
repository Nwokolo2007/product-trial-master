using AltenShop.Domain.Exceptions;
using AltenShop.Domain.ValueObjects;

namespace AltenShop.Domain.Entities.Identity
{
	public class User
	{
		public Guid Id { get; private set; } = Guid.NewGuid();
		public string Username { get; private set; } = default!;
		public EmailAddress Email { get; private set; } = default!;
		public PasswordHash Password { get; private set; } = default!;
		public FullName FullName { get; private set; } = default!;
		public bool IsAdmin { get; private set; }
		public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;

		private User() { }

		public User(string username, EmailAddress email, PasswordHash password, FullName fullName, bool isAdmin = false)
		{
			if (string.IsNullOrWhiteSpace(username))
				throw new DomainException("Username is required.");
			Username = username.Trim();
			Email = email ?? throw new DomainException("Email is required.");
			Password = password ?? throw new DomainException("Password is required.");
			FullName = fullName ?? throw new DomainException("Full name is required.");
			IsAdmin = isAdmin || IsAdminEmail(email);
		}

		private static bool IsAdminEmail(EmailAddress email)
			=> string.Equals(email.Value, "admin@admin.com", StringComparison.OrdinalIgnoreCase);


		public static User Register(string username, string firstName, string lastName, string email, string password)
		{
			var emailAddress = new EmailAddress(email);
			var fullName = new FullName(firstName, lastName);
			var passwordHash = PasswordHash.Create(password);
			return new User(username, emailAddress, passwordHash, fullName);
		}
		public bool VerifyPassword(string plainPassword) => Password.Verify(plainPassword);

		public bool CanManageProducts() => IsAdmin;
	}
}
