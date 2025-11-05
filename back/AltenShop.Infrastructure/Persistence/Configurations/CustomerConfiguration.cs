using AltenShop.Domain.Entities.Commerce;
using AltenShop.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AltenShop.Infrastructure.Persistence.Configurations
{
	public sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
	{
		public void Configure(EntityTypeBuilder<Customer> builder)
		{
			builder.ToTable("Customers");
			builder.HasKey(c => c.Id);

			builder.Property(c => c.UserId).IsRequired();
			builder.HasIndex(c => c.UserId).IsUnique();

			// Configure Email and FullName as owned value objects
			builder.OwnsOne(c => c.Email, email =>
			{
				email.Property(e => e.Value)
					.IsRequired()
					.HasColumnName("Email");
			});

			builder.OwnsOne(c => c.FullName, fullName =>
			{
				fullName.Property(f => f.First)
					.IsRequired()
					.HasColumnName("FirstName");
				fullName.Property(f => f.Last)
					.IsRequired()
					.HasColumnName("LastName");
			});

			// Configure Address value objects
			builder.OwnsOne(c => c.DefaultShippingAddress, address =>
			{
				address.Property(a => a.Street).HasColumnName("Shipping_Street");
				address.Property(a => a.City).HasColumnName("Shipping_City");
				address.Property(a => a.PostalCode).HasColumnName("Shipping_PostalCode");
				address.Property(a => a.Country).HasColumnName("Shipping_Country");
			});

			builder.OwnsOne(c => c.DefaultBillingAddress, address =>
			{
				address.Property(a => a.Street).HasColumnName("Billing_Street");
				address.Property(a => a.City).HasColumnName("Billing_City");
				address.Property(a => a.PostalCode).HasColumnName("Billing_PostalCode");
				address.Property(a => a.Country).HasColumnName("Billing_Country");
			});
		}
	}
}
