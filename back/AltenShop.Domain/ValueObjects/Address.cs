using AltenShop.Domain.Exceptions;

namespace AltenShop.Domain.ValueObjects
{
	public sealed record Address
	{
		public string Street { get; }
		public string City { get; }
		public string PostalCode { get; }
		public string Country { get; }

		public Address(string street, string city, string postalCode, string country)
		{
			if (string.IsNullOrWhiteSpace(street))
				throw new DomainException("Street is required.");
			if (string.IsNullOrWhiteSpace(city))
				throw new DomainException("City is required.");
			if (string.IsNullOrWhiteSpace(postalCode))
				throw new DomainException("Postal code is required.");
			if (string.IsNullOrWhiteSpace(country))
				throw new DomainException("Country is required.");

			Street = street.Trim();
			City = city.Trim();
			PostalCode = postalCode.Trim();
			Country = country.Trim();
		}

		public override string ToString() => $"{Street}, {City}, {PostalCode}, {Country}";
	}
}

