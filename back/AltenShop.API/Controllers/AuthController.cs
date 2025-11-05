using AltenShop.Application.Features.Accounts.Commands;
using AltenShop.Application.Features.Accounts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AltenShop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("1.0")]
public class AuthController : ControllerBase
{
	private readonly IMediator _mediator;
	public AuthController(IMediator mediator) => _mediator = mediator;

	[HttpPost("token")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Login([FromBody] AuthenticateUserQuery command)
	{
		var token = await _mediator.Send(command);
		return Ok(new { token });
	}
}
