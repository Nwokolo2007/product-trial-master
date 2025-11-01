using AltenShop.Domain.Exceptions;

namespace AltenShop.Domain.ValueObjects
{
	public sealed record FullName
	{
		public string First { get; }
		public string Last { get; }

		public FullName(string firstName, string lastName)
		{
			if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
				throw new DomainException("full name parts are required");
			First = firstName.Trim(); Last = lastName.Trim();
		}

		public override string ToString() => $"{First} {Last}";
	}
}
