using AltenShop.Domain.Entities.Commerce;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AltenShop.Infrastructure.Persistence.Configurations;

public sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
	public void Configure(EntityTypeBuilder<Customer> builder)
	{
		builder.ToTable("Customers");
		builder.HasKey(c => c.Id);

		builder.Property(c => c.UserId).IsRequired();
		//builder.Property(c => c.PreferredLanguage).HasMaxLength(5);
		//builder.Property(c => c.Country).HasMaxLength(100);

		builder.HasIndex(c => c.UserId).IsUnique(); // 1:1 User-Customer mapping
	}
}
