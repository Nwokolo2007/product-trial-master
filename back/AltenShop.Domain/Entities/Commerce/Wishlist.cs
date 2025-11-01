using AltenShop.Domain.Exceptions;

namespace AltenShop.Domain.Entities.Commerce
{
	public sealed class Wishlist
	{
		public Guid Id { get; private set; } = Guid.NewGuid();
		public Guid CustomerId { get; private set; }
		public DateTime UpdatedUtc { get; private set; }

		private readonly List<WishlistItem> _items = new();

		public IReadOnlyCollection<WishlistItem> Items => _items.AsReadOnly();

		private Wishlist() { }

		public Wishlist(Guid customerId)
		{
			if (customerId == Guid.Empty)
				throw new DomainException("CustomerId is required.");
			CustomerId = customerId;

		}

		public void AddProduct(int productId)
		{
			if (productId <= 0)
				throw new DomainException($"Invalid productId {productId}");
			if (_items.Any(x => x.ProductId == productId)) return;
			_items.Add(new WishlistItem(productId));

			UpdatedUtc = DateTime.UtcNow;
		}

		public void RemoveProduct(int productId)
		{
			var item = _items.FirstOrDefault(x => x.ProductId == productId);
			if (item is null) return;

			_items.Remove(item);

			UpdatedUtc = DateTime.UtcNow;
		}

		public bool ContainsProduct(int productId)
		=> _items.Any(item => item.ProductId == productId);
	}
}
