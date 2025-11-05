using AltenShop.Application.Features.Accounts.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AltenShop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("1.0")]
public class AccountController : ControllerBase
{
	private readonly IMediator _mediator;
	public AccountController(IMediator mediator) => _mediator = mediator;

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
	{
		var id = await _mediator.Send(command);
		return CreatedAtAction(nameof(Register), new { id }, new { message = "Account created successfully", id });
	}
}
