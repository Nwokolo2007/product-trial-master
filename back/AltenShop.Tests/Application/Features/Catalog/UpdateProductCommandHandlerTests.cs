using AltenShop.Application.Common.Exceptions;
using AltenShop.Application.Features.Catalog.Commands;
using AltenShop.Application.Ports.Repositories;
using AltenShop.Domain.Entities.Commerce;
using FluentAssertions;
using Moq;

namespace AltenShop.Tests.Application.Features.Catalog
{

	public class UpdateProductCommandHandlerTests
	{

		[Fact]
		public async Task Update_Existing_Product_Fields()
		{
			// Arrange
			var repoMock = new Mock<IProductRepository>();
			var product = Product.Create("P001", "Test", "desc", 10m, 5, "Cat", "", "REF001", 1);

			repoMock.Setup(r => r.GetByIdAsync(product.Id, default))
					.ReturnsAsync(product);

			var handler = new UpdateProductCommandHandler(repoMock.Object);
			var command = new UpdateProductCommand(
			   product.Id,
			   product.Name,
			   product.Description,
			   product.ImageUrl,
			   product.Category,
			   20m,   // new price
			   10     // new quantity
		   );

			// Act
			await handler.Handle(command, default);

			// Assert
			product.Price.Should().Be(20m);
			product.Quantity.Should().Be(10);
			repoMock.Verify(r => r.UpdateAsync(product, default), Times.Once);
		}

		[Fact]
		public async Task Throw_Exception__When_Product_Not_Found()
		{
			// Arrange
			var repoMock = new Mock<IProductRepository>();
			repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>(), default))
					.ReturnsAsync((Product?)null);

			var handler = new UpdateProductCommandHandler(repoMock.Object);
			var command = new UpdateProductCommand(
				999,
				"Name",
				"Description",
				"ImageUrl",
				"Category",
				15m,
				5
			);

			// Act
			Func<Task> act = async () => await handler.Handle(command, default);

			// Assert
			await act.Should().ThrowAsync<NotFoundException>();
		}
	}
}

