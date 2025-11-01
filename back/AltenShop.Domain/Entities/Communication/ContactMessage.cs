using AltenShop.Domain.Exceptions;
using AltenShop.Domain.ValueObjects;

namespace AltenShop.Domain.Entities.Communication
{
	public class ContactMessage
	{
		public Guid Id { get; private set; } = Guid.NewGuid();
		public Guid CustomerId { get; private set; }
		public EmailAddress Email { get; private set; }
		public string Message { get; private set; }

		public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;

		private ContactMessage() { }

		public ContactMessage(Guid customerId, EmailAddress email, string message)
		{

			if (string.IsNullOrWhiteSpace(message))
				throw new DomainException("Message cannot be empty.");
			if (message.Length > 300)
				throw new DomainException("Message cannot exceed 300 characters.");

			CustomerId = customerId;
			Email = email;
			Message = message.Trim();
		}
	}
}
