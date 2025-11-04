using AltenShop.Domain.Entities.Commerce;
using AltenShop.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AltenShop.Infrastructure.Persistence.Configurations;

public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
	public void Configure(EntityTypeBuilder<Product> builder)
	{
		builder.ToTable("Products");

		builder.HasKey(p => p.Id);

		builder.Property(p => p.Code)
			   .IsRequired()
			   .HasMaxLength(64);

		builder.Property(p => p.Name)
			   .IsRequired()
			   .HasMaxLength(256);

		builder.Property(p => p.Category)
			   .HasMaxLength(128);

		builder.Property(p => p.Description)
			   .HasMaxLength(2000);

		builder.Property(p => p.ImageUrl)
			   .HasMaxLength(512);

		builder.Property(p => p.Price)
			   .HasPrecision(18, 2);

		builder.Property(p => p.InternalReference)
			   .HasMaxLength(64)
			   .IsRequired();

		builder.Property(p => p.InventoryStatus)
			   .HasConversion(
					v => v.Value,
					v => InventoryStatus.FromString(v))
			   .HasMaxLength(20);

		builder.Property(p => p.CreatedAtUtc)
			   .HasDefaultValueSql("CURRENT_TIMESTAMP");

		builder.Property(p => p.UpdatedAtUtc)
			   .HasDefaultValueSql("CURRENT_TIMESTAMP");
	}
}
