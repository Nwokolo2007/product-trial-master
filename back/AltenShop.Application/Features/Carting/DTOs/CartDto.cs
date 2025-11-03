namespace AltenShop.Application.Features.Carting.DTOs
{
	public record CartDto(Guid Id, Guid CustomerId, IReadOnlyList<CartItemDto> Items, decimal Total);
}
