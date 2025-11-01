using AltenShop.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace AltenShop.Domain.ValueObjects
{
	public sealed record EmailAddress
	{
		private static readonly Regex Pattern = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
		public string Value { get; }

		public EmailAddress(string value)
		{
			if (string.IsNullOrWhiteSpace(value) || !Pattern.IsMatch(value))
				throw new DomainException("Invalid email format.");
			Value = value.Trim().ToLowerInvariant();
		}

		public override string ToString() => Value;

	}
}
