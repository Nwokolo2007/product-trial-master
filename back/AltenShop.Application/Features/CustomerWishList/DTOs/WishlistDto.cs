namespace AltenShop.Application.Features.Wishlist.DTOs
{
	public record WishlistItemDto(int ProductId);
	public record WishlistDto(Guid Id, Guid CustomerId, IReadOnlyList<WishlistItemDto> Items);

}
