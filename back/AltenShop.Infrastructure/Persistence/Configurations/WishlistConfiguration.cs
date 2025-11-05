using AltenShop.Domain.Entities.Commerce;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AltenShop.Infrastructure.Persistence.Configurations;

public sealed class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
{
	public void Configure(EntityTypeBuilder<Wishlist> builder)
	{
		builder.ToTable("Wishlists");
		builder.HasKey(w => w.Id);
		builder.Property(w => w.CustomerId).IsRequired();

		builder.OwnsMany(w => w.Items, item =>
		{
			item.ToTable("WishlistItems");
			item.WithOwner().HasForeignKey("WishlistId");
			item.HasKey("WishlistId", "ProductId");
			item.Property(i => i.ProductId).IsRequired();
		});
	}
}
