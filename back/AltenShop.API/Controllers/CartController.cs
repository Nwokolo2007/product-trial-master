using AltenShop.Application.Features.Carting.Commands;
using AltenShop.Application.Features.Carting.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AltenShop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CartController : ControllerBase
{
	private readonly IMediator _mediator;
	public CartController(IMediator mediator) => _mediator = mediator;

	[HttpGet]
	public async Task<IActionResult> GetCart()
	{
		var customerId = GetUserId();
		var cart = await _mediator.Send(new GetCartByCustomerIdQuery(customerId));
		return Ok(cart);
	}

	[HttpPost("add")]
	public async Task<IActionResult> Add([FromBody] AddToCartCommand command)
	{
		var customerId = GetUserId();
		command = command with { CustomerId = customerId };
		await _mediator.Send(command);
		return Ok(new { message = "Product added to cart" });
	}

	[HttpDelete("{productId}")]
	public async Task<IActionResult> Remove(int productId)
	{
		var customerId = GetUserId();
		await _mediator.Send(new RemoveFromCartCommand(customerId, productId));
		return NoContent();
	}

	private Guid GetUserId()
		=> Guid.TryParse(User.FindFirst("sub")?.Value, out var id) ? id : Guid.Empty;
}
