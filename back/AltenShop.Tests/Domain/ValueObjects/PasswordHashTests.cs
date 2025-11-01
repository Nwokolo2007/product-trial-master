using AltenShop.Domain.ValueObjects;
using FluentAssertions;

namespace AltenShop.Tests.Domain.ValueObjects
{
	public class PasswordHashTests
	{
		[Fact]
		public void Hash_And_Verify_Password_Correctly()
		{
			var password = "P@ssw0rd";
			var hash = PasswordHash.Create(password);

			hash.Verify(password).Should().BeTrue();
		}

		[Fact]
		public void Verificatio_Fail_For_Wrong_Password()
		{
			var password = "CorrectPassword!";
			var hash = PasswordHash.Create(password);

			hash.Verify("WrongPassword!").Should().BeFalse();

		}

		[Fact]
		public void Passwords_Are_Immutable()
		{
			var password = "Immutable123!";
			var hash1 = PasswordHash.Create(password);
			var hash2 = PasswordHash.Create(password);

			hash1.Should().NotBeSameAs(hash2);
			hash1.Should().NotBe(hash2); // different salt every time

		}
	}
}
