using AltenShop.Domain.Exceptions;

namespace AltenShop.Domain.Entities.Commerce
{
	public sealed class WishlistItem : IEquatable<WishlistItem>
	{
		public int ProductId { get; private set; }

		private WishlistItem() { }
		public WishlistItem(int productId)
		{
			if (productId <= 0)
				throw new DomainException("Invalid product ID.");
			ProductId = productId;
		}

		public bool Equals(WishlistItem? other)

			=> other is not null && ProductId == ProductId;

		public override bool Equals(object? obj) => Equals(obj as WishlistItem);

		public override int GetHashCode() => ProductId.GetHashCode();


	}
}
