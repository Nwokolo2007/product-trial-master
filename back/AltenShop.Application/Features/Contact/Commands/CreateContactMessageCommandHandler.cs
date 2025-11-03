using MediatR;

namespace AltenShop.Application.Features.Contact.Commands;

// NOTE: persist via a ContactRepository if you create one.
// .
public sealed class CreateContactMessageCommandHandler : IRequestHandler<CreateContactMessageCommand, Guid>
{
	public Task<Guid> Handle(CreateContactMessageCommand r, CancellationToken ct)
	{
		// Example domain creation (implement ContactMessage in Domain if needed)
		// var cm = new ContactMessage(new EmailAddress(r.Email), r.Message);
		// save via repository...
		return Task.FromResult(Guid.NewGuid()); // replace with real persistence
	}
}
