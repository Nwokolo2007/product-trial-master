using AltenShop.Domain.Entities.Commerce;
using AltenShop.Domain.Exceptions;
using FluentAssertions;

namespace AltenShop.Tests.Domain.Commerce
{
	public class ProductTests
	{
		[Fact]
		public void Create_Product_With_Valid_Data()
		{
			var product = Product.Create(
			code: "P100",
			name: "Wireless Mouse",
			category: "Accessories",
			price: 25.99m,
			quantity: 50);

			product.Code.Should().Be("P100");
			product.Name.Should().Be("Wireless Mouse");
			product.Category.Should().Be("Accessories");
			product.Price.Should().Be(25.99m);
			product.Quantity.Should().Be(50);
			product.InventoryStatus.Value.Should().Be("INSTOCK");
		}

		[Fact]
		public void Do_Not_Create_Products_Without_Code()
		{
			Action act = () => Product.Create(
				code: "",
				name: "Invalid Product",
				category: "Test",
				price: 10,
				quantity: 5
			);

			act.Should().Throw<DomainException>()
			   .WithMessage("*Product code is required*");
		}

		[Theory]
		[InlineData(15, "INSTOCK")]
		[InlineData(5, "LOWSTOCK")]
		[InlineData(0, "OUTOFSTOCK")]
		public void Assign_Correct_Inventory_Status_Based_On_Quantity(int qty, string expected)
		{
			var product = Product.Create("P101", "Test", "Cat", 10m, qty);

			product.InventoryStatus.Value.Should().Be(expected);
		}
	}
}

