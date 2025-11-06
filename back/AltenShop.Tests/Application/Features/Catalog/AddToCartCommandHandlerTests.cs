using AltenShop.Application.Features.Carting.Commands;
using AltenShop.Application.Ports.Repositories;
using AltenShop.Domain.Entities.Commerce;
using FluentAssertions;
using Moq;

namespace AltenShop.Tests.Application.Features.Carting;

public class AddToCartCommandHandlerTests
{
	[Fact]
	public async Task Add_New_Item_To_Cart()
	{
		// Arrange
		var repoMock = new Mock<ICartRepository>();
		var repoMockProd = new Mock<IProductRepository>();
		var existingCart = new Cart(Guid.NewGuid());
		repoMock.Setup(r => r.GetByCustomerIdAsync(existingCart.CustomerId, default))
				.ReturnsAsync(existingCart);

		var handler = new AddToCartCommandHandler(repoMock.Object, repoMockProd.Object);
		var command = new AddToCartCommand(existingCart.CustomerId, 1, 2, 100m);

		// Act
		await handler.Handle(command, default);

		// Assert
		existingCart.Items.Should().HaveCount(1);
		existingCart.Total.Should().Be(200m);
		repoMock.Verify(r => r.UpdateAsync(existingCart, default), Times.Once);
	}
}
