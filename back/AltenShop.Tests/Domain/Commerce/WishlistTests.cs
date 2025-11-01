using AltenShop.Domain.Entities.Commerce;
using AltenShop.Domain.Exceptions;
using FluentAssertions;

namespace AltenShop.Tests.Domain.Commerce
{
	public class WishlistTests
	{
		[Fact]
		public void Add_New_Product_To_Wishlist()
		{
			var wishlist = new Wishlist(Guid.NewGuid());
			wishlist.AddProduct(1);

			wishlist.Items.Should().ContainSingle(x => x.ProductId == 1);
		}

		[Fact]
		public void Do_Not_Add_Duplicate_Product()
		{
			var wishlist = new Wishlist(Guid.NewGuid());
			wishlist.AddProduct(1);
			wishlist.AddProduct(1);

			wishlist.Items.Should().HaveCount(1);
		}

		[Fact]
		public void Remove_Product_From_Wishlist()
		{
			var wishlist = new Wishlist(Guid.NewGuid());
			wishlist.AddProduct(1);
			wishlist.AddProduct(2);

			wishlist.RemoveProduct(1);

			wishlist.Items.Should().ContainSingle(x => x.ProductId == 2);
		}

		[Fact]
		public void Should_Throw_When_CustomerId_Is_Empty()
		{
			Action act = () => new Wishlist(Guid.Empty);

			act.Should().Throw<DomainException>()
			   .WithMessage("*CustomerId is required*");
		}
	}
}
