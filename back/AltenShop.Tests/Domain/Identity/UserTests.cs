using AltenShop.Domain.Entities.Identity;
using AltenShop.Domain.Exceptions;
using AltenShop.Domain.ValueObjects;
using FluentAssertions;

namespace AltenShop.Tests.Domain.Identity
{
	public class UserTests
	{
		[Fact]
		public void Create_User_With_Valid_Data()
		{
			//Arrange
			var user = new User(
			username: "buzo",
			email: new EmailAddress("cnwokolo@alten.com"),
			password: PasswordHash.Create("MyStrongPass!"),
			fullName: new FullName("Chibuzo", "Nwokolo"));

			user.Username.Should().Be("buzo");
			user.Email.Value.Should().Be("cnwokolo@alten.com");
			user.FullName.ToString().Should().Be("Chibuzo Nwokolo");
			user.IsAdmin.Should().BeFalse();
		}

		[Fact]
		public void Do_Not_Allow_Empty_UserName()
		{
			Action act = () => new User(
			username: "",
			email: new EmailAddress("user@example.com"),
			password: PasswordHash.Create("StrongPass!"),
			fullName: new FullName("Danny", "Charles"));

			act.Should().Throw<DomainException>()
				.WithMessage("*Username is required*");
		}

		[Fact]
		public void Hash_And_Verify_Password_Correctly()
		{
			var password = "SecretPassword!";
			var user = new User(
			   username: "janedoe",
			   email: new EmailAddress("jane@alten.com"),
			   password: PasswordHash.Create(password),
			   fullName: new FullName("Jane", "Daniella"));

			user.VerifyPassword(password).Should().BeTrue();
			user.VerifyPassword("WrongPass!").Should().BeFalse();
		}

		[Fact]
		public void Mark_As_Admin_When_Email_Is_Admin()
		{
			var user = new User(
				username: "admin",
				email: new EmailAddress("admin@admin.com"),
				password: PasswordHash.Create("StrongPass!"),
				fullName: new FullName("Super", "Admin")
			);

			user.IsAdmin.Should().BeTrue();
		}
	}
}
