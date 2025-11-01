using AltenShop.Domain.Exceptions;
using AltenShop.Domain.ValueObjects;
using FluentAssertions;

namespace AltenShop.Tests.Domain.ValueObjects
{
	public class EmailAddressTests
	{
		[Theory]
		[InlineData("user@alten.com")]
		[InlineData("user@ALTEN.com")]
		public void EmailAddress_Should_Be_Normalized_And_Comparable(string email)
		{
			var address = new EmailAddress(email);

			address.Value.Should().Be(email.ToLowerInvariant());
		}

		[Theory]
		[InlineData("invalid")]
		[InlineData("noatsymbol.com")]
		[InlineData("user@.com")]
		public void Do_Not_Allow_Invalid_Emails(string invalidEmail)
		{
			Action act = () => new EmailAddress(invalidEmail);

			act.Should().Throw<DomainException>()
			   .WithMessage("*Invalid email format*");
		}

		[Fact]
		public void Equal_Emails_Should_Be_Considered_Equal()
		{
			var a = new EmailAddress("same@domain.com");
			var b = new EmailAddress("Same@Domain.com");

			a.Should().Be(b);
		}

	}
}
