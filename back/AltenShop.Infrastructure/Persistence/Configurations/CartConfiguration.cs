using AltenShop.Domain.Entities.Commerce;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AltenShop.Infrastructure.Persistence.Configurations
{
	public sealed class CartConfiguration : IEntityTypeConfiguration<Cart>
	{
		public void Configure(EntityTypeBuilder<Cart> builder)
		{
			builder.ToTable("Carts");

			builder.HasKey(c => c.Id);
			builder.Property(c => c.CustomerId).IsRequired();

			builder.OwnsMany(c => c.Items, item =>
			{
				item.ToTable("CartItems");
				item.WithOwner().HasForeignKey("CartId");

				item.HasKey("CartId", "ProductId");

				item.Property(i => i.ProductId).IsRequired();
				item.Property(i => i.Quantity).IsRequired();
				item.Property(i => i.UnitPrice).HasPrecision(18, 2);

				item.Ignore(i => i.SubTotal);
			});
		}
	}
}
