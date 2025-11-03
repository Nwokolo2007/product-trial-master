namespace AltenShop.Application.Features.Catalog.DTOs
{
	public record ProductDto(
	   int Id,
	   string Code,
	   string Name,
	   string Description,
	   string ImageUrl,
	   string Category,
	   decimal Price,
	   int Quantity,
	   string InventoryStatus,
	   double Rating
   );
}
