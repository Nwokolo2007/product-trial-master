namespace AltenShop.Application.Features.Carting.DTOs
{
	public record CartItemDto(int ProductId, int Quantity, decimal UnitPrice, decimal SubTotal);
}
