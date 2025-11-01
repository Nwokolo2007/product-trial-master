using AltenShop.Domain.Exceptions;

namespace AltenShop.Domain.Entities.Commerce
{
	public sealed class Cart
	{
		public Guid Id { get; private set; } = Guid.NewGuid();
		public Guid CustomerId { get; private set; }

		private readonly List<CartItem> _items = new();
		public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

		public DateTime UpdatedAtUtc { get; private set; } = DateTime.UtcNow;

		private Cart() { } // EF Core requirement

		public Cart(Guid customerId)
		{
			if (customerId == Guid.Empty)
				throw new DomainException("CustomerId is required.");
			CustomerId = customerId;
		}

		public void AddItem(int productId, int quantity, decimal unitPrice)
		{
			if (quantity <= 0)
				throw new DomainException("Quantity must be greater than zero.");
			if (unitPrice < 0)
				throw new DomainException("Unit price cannot be negative.");

			var existing = _items.FirstOrDefault(x => x.ProductId == productId);
			if (existing is null)
			{
				_items.Add(new CartItem(productId, quantity, unitPrice));
			}
			else
			{
				existing.IncreaseQuantity(quantity);
			}

			UpdatedAtUtc = DateTime.UtcNow;
		}

		public void RemoveItem(int productId)
		{
			var item = _items.FirstOrDefault(x => x.ProductId == productId);
			if (item is null)
				return;

			_items.Remove(item);
			UpdatedAtUtc = DateTime.UtcNow;
		}

		public void Clear()
		{
			_items.Clear();
			UpdatedAtUtc = DateTime.UtcNow;
		}

		public decimal Total => _items.Sum(i => i.SubTotal);

		public bool ContainsProduct(int productId) => _items.Any(i => i.ProductId == productId);
	}

}
