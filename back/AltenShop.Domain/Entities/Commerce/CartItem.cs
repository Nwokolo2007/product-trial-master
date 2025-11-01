using AltenShop.Domain.Exceptions;

namespace AltenShop.Domain.Entities.Commerce
{
	public sealed class CartItem : IEquatable<CartItem>
	{
		public int ProductId { get; private set; }
		public int Quantity { get; private set; }
		public decimal UnitPrice { get; private set; }
		public decimal SubTotal => Quantity * UnitPrice;

		private CartItem() { } // EF Core

		public CartItem(int productId, int quantity, decimal unitPrice)
		{
			if (productId <= 0)
				throw new DomainException("Invalid product ID.");
			if (quantity <= 0)
				throw new DomainException("Quantity must be greater than zero.");
			if (unitPrice < 0)
				throw new DomainException("Price cannot be negative.");

			ProductId = productId;
			Quantity = quantity;
			UnitPrice = unitPrice;
		}

		public void IncreaseQuantity(int amount)
		{
			if (amount <= 0)
				throw new DomainException("Increase amount must be positive.");

			Quantity += amount;
		}

		public bool Equals(CartItem? other) =>
			other is not null && ProductId == other.ProductId;

		public override bool Equals(object? obj) => Equals(obj as CartItem);

		public override int GetHashCode() => ProductId.GetHashCode();
	}

}
