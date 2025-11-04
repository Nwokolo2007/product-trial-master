using AltenShop.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AltenShop.Infrastructure.Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.ToTable("Users");

		builder.HasKey(u => u.Id);

		builder.Property(u => u.Username)
			   .IsRequired()
			   .HasMaxLength(100);

		builder.OwnsOne(u => u.Email, e =>
		{
			e.Property(x => x.Value)
			 .IsRequired()
			 .HasMaxLength(255)
			 .HasColumnName("Email");
		});

		builder.OwnsOne(u => u.Password, p =>
		{
			p.Property(x => x.Hash)
			 .IsRequired()
			 .HasMaxLength(255)
			 .HasColumnName("PasswordHash");
		});

		builder.OwnsOne(u => u.FullName, f =>
		{
			f.Property(x => x.First)
			 .HasMaxLength(100)
			 .HasColumnName("FirstName");
			f.Property(x => x.Last)
			 .HasMaxLength(100)
			 .HasColumnName("LastName");
		});

		builder.Property(u => u.IsAdmin)
			   .HasDefaultValue(false);

		builder.Property(u => u.CreatedAtUtc)
			   .HasDefaultValueSql("CURRENT_TIMESTAMP");
	}
}
