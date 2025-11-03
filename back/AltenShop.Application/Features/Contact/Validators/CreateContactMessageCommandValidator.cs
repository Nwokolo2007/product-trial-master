using FluentValidation;

namespace AltenShop.Application.Features.Contact.Commands;

public sealed class CreateContactMessageCommandValidator : AbstractValidator<CreateContactMessageCommand>
{
	public CreateContactMessageCommandValidator()
	{
		RuleFor(x => x.Email).NotEmpty().EmailAddress();
		RuleFor(x => x.Message).NotEmpty().MaximumLength(300);
	}
}
