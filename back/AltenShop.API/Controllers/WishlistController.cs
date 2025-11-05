using AltenShop.Application.Features.Wishlist.Commands;
using AltenShop.Application.Features.Wishlist.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AltenShop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WishlistController : ControllerBase
{
	private readonly IMediator _mediator;
	public WishlistController(IMediator mediator) => _mediator = mediator;

	[HttpGet]
	public async Task<IActionResult> Get()
	{
		var customerId = GetUserId();
		var wishlist = await _mediator.Send(new GetWishlistByCustomerIdQuery(customerId));
		return Ok(wishlist);
	}

	[HttpPost("add")]
	public async Task<IActionResult> Add([FromBody] AddToWishlistCommand command)
	{
		var customerId = GetUserId();
		command = command with { CustomerId = customerId };
		await _mediator.Send(command);
		return Ok(new { message = "Product added to wishlist" });
	}

	[HttpDelete("{productId}")]
	public async Task<IActionResult> Remove(int productId)
	{
		var customerId = GetUserId();
		await _mediator.Send(new RemoveFromWishlistCommand(customerId, productId));
		return NoContent();
	}

	private Guid GetUserId()
		=> Guid.TryParse(User.FindFirst("sub")?.Value, out var id) ? id : Guid.Empty;
}
