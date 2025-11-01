using AltenShop.Domain.Exceptions;
using AltenShop.Domain.ValueObjects;

namespace AltenShop.Domain.Entities.Commerce
{
	/// <summary>
	/// Represents a business customer profile (linked to a system user).
	/// </summary>
	public sealed class Customer
	{
		public Guid Id { get; private set; } = Guid.NewGuid();
		public Guid UserId { get; private set; }
		public EmailAddress Email { get; private set; } = default!;
		public FullName FullName { get; private set; } = default!;
		public Address? DefaultShippingAddress { get; private set; }
		public Address? DefaultBillingAddress { get; private set; }
		public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;
		public DateTime UpdatedAtUtc { get; private set; } = DateTime.UtcNow;
		private Customer() { } // This is used for Entity Framework

		public Customer(Guid userId, EmailAddress email, FullName fullName)
		{
			if (userId == Guid.Empty)
				throw new DomainException("UserId is required.");

			UserId = userId;
			Email = email ?? throw new DomainException("Email is required.");
			FullName = fullName ?? throw new DomainException("Full name is required.");
		}

		public void UpdateShippingAddress(Address address)
		{
			DefaultShippingAddress = address ?? throw new DomainException("Address required.");
			UpdatedAtUtc = DateTime.UtcNow;
		}

		public void UpdateBillingAddress(Address address)
		{
			DefaultBillingAddress = address ?? throw new DomainException("Address required.");
			UpdatedAtUtc = DateTime.UtcNow;
		}
	}
}
