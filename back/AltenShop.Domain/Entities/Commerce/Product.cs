using AltenShop.Domain.Exceptions;
using AltenShop.Domain.ValueObjects;

namespace AltenShop.Domain.Entities.Commerce
{
	public class Product
	{
		public int Id { get; private set; }                     // DB-generated identity
		public string Code { get; private set; } = default!;
		public string Name { get; private set; } = default!;
		public string Description { get; private set; } = default!;
		public string ImageUrl { get; private set; } = default!;
		public string Category { get; private set; } = default!;
		public decimal Price { get; private set; }
		public int Quantity { get; private set; }
		public string InternalReference { get; private set; } = default!;
		public int ShellId { get; private set; }
		public InventoryStatus InventoryStatus { get; private set; } = InventoryStatus.OutOfStock;
		public double Rating { get; private set; }
		public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;
		public DateTime UpdatedAtUtc { get; private set; } = DateTime.UtcNow;


		private Product() { }

		private Product(
		string code,
		string name,
		string description,
		string imageUrl,
		string category,
		decimal price,
		int quantity,
		string internalReference,
		int shellId,
		double rating)
		{
			if (string.IsNullOrWhiteSpace(code))
				throw new DomainException("Product code is required.");

			if (price < 0)
				throw new DomainException("Price cannot be negative.");

			if (quantity < 0)
				throw new DomainException("Quantity cannot be negative.");

			Code = code.Trim();
			Name = name.Trim();
			Description = description?.Trim() ?? string.Empty;
			ImageUrl = imageUrl?.Trim() ?? string.Empty;
			Category = category?.Trim() ?? "General";
			Price = price;
			Quantity = quantity;
			InternalReference = internalReference?.Trim() ?? string.Empty;
			ShellId = shellId;
			Rating = Math.Clamp(rating, 0.0, 5.0);
			InventoryStatus = InventoryStatus.FromQuantity(quantity);
		}



		public static Product Create(
		string code,
		string name,
		string category,
		decimal price,
		int quantity,
		string? description = null,
		string? imageUrl = null,
		string? internalReference = null,
		int? shellId = null,
		double rating = 0.0)
		{
			return new Product(
				code: code,
				name: name,
				description: description ?? string.Empty,
				imageUrl: imageUrl ?? string.Empty,
				category: category,
				price: price,
				quantity: quantity,
				internalReference: internalReference ?? Guid.NewGuid().ToString("N"),
				shellId: shellId ?? 0,
				rating: rating
			);
		}
		public void UpdateQuantity(int quantity)
		{
			if (quantity < 0)
				throw new DomainException("Quantity cannot be negative.");
			Quantity = quantity;
			InventoryStatus = InventoryStatus.FromQuantity(Quantity);
			UpdatedAtUtc = DateTime.UtcNow;
		}

		public void UpdatePrice(decimal price)
		{
			if (price < 0) throw new DomainException("Price cannot be negative.");
			Price = price;
			UpdatedAtUtc = DateTime.UtcNow;
		}

		public void UpdateDetails(string? name, string? description, string? category, string? imageUrl)
		{
			if (!string.IsNullOrWhiteSpace(name))
				Name = name.Trim();

			if (description is not null)
				Description = description.Trim();

			if (!string.IsNullOrWhiteSpace(category))
				Category = category.Trim();

			if (imageUrl is not null)
				ImageUrl = imageUrl.Trim();

			UpdatedAtUtc = DateTime.UtcNow;
		}

	}
}
