using AltenShop.Application.Features.Contact.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AltenShop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
	private readonly IMediator _mediator;
	public ContactController(IMediator mediator) => _mediator = mediator;

	[HttpPost]
	public async Task<IActionResult> Send([FromBody] CreateContactMessageCommand command)
	{
		await _mediator.Send(command);
		return Ok(new { message = "Demande de contact envoyée avec succès" });
	}
}
