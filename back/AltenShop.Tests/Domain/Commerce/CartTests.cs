using AltenShop.Domain.Entities.Commerce;
using AltenShop.Domain.Exceptions;
using FluentAssertions;

namespace AltenShop.Tests.Domain.Commerce
{
	public class CartTests
	{
		[Fact]
		public void Add_New_Item_When_Product_Not_In_Cart()
		{
			//Arrange
			var cart = new Cart(Guid.NewGuid());
			//Act
			cart.AddItem(productId: 1, quantity: 2, unitPrice: 100m);

			//Assert
			cart.Items.Should().HaveCount(1);
			cart.Total.Should().Be(200m);
		}

		[Fact]
		public void Increase_Quantity_Of_Existing_Cart_Item()
		{
			//Arrange
			var cart = new Cart(Guid.NewGuid());
			cart.AddItem(1, 1, 50m);
			//Act
			cart.AddItem(1, 2, 50m);
			//Assert
			cart.Items.Should().HaveCount(1);
			cart.Total.Should().Be(150m);
		}
		[Fact]
		public void Cannot_Add_Item_With_Zero_Quantity()
		{
			//Arrange
			var cart = new Cart(Guid.NewGuid());
			//Act
			Action act = () => cart.AddItem(1, 0, 100m);
			//Assert
			act.Should().Throw<DomainException>()
				.WithMessage("*greater than zero*");
		}

		[Fact]
		public void Remove_Existing_Item_From_Cart()
		{
			//Arrange
			var cart = new Cart(Guid.NewGuid());
			cart.AddItem(1, 1, 100m);
			cart.AddItem(2, 1, 200m);
			//Act
			cart.RemoveItem(1);
			//Assert
			cart.Items.Should().HaveCount(1);
			cart.Total.Should().Be(200m);
		}

		[Fact]
		public void Clear_Cart()
		{
			//Arrange
			var cart = new Cart(Guid.NewGuid());
			cart.AddItem(1, 1, 100m);
			cart.AddItem(2, 1, 200m);

			//Act
			cart.Clear();
			//Assert
			cart.Items.Should().BeEmpty();
			cart.Total.Should().Be(0m);
		}

	}
}
