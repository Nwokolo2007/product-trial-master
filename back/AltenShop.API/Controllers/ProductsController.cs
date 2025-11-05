using AltenShop.Application.Features.Catalog.Commands;
using AltenShop.Application.Features.Catalog.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AltenShop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("1.0")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProductsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll([FromQuery] GetProductsQuery query)
    {
        var products = await _mediator.Send(query);
        return Ok(products);
    }

	[Authorize(Policy = "AdminEmailOnly")]
	[HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        //if (!User.Identity!.IsAuthenticated) return Unauthorized();
        //var email = User.Identity!.Name;
        //if (email?.ToLower() != "admin@admin.com")
        //    return Forbid();

        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAll), new { id }, new { id });
    }

	[Authorize(Policy = "AdminEmailOnly")]
	[HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductCommand command)
    {
        if (id != command.Id) return BadRequest();
        //if (User.Identity!.Name?.ToLower() != "admin@admin.com") return Forbid();

        await _mediator.Send(command);
        return NoContent();
    }

	[Authorize(Policy = "AdminEmailOnly")]
	[HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        //if (User.Identity!.Name?.ToLower() != "admin@admin.com") return Forbid();

        await _mediator.Send(new DeleteProductCommand(id));
        return NoContent();
    }
}
