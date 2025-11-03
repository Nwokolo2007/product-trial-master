using AltenShop.Application.Features.Catalog.Commands;
using FluentValidation;

namespace AltenShop.Application.Features.Accounts.Validators
{
	public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
	{
		public CreateProductCommandValidator()
		{
			RuleFor(x => x.Code).NotEmpty().MaximumLength(64);
			RuleFor(x => x.Name).NotEmpty().MaximumLength(256);
			RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
			RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);
			RuleFor(x => x.Category).NotEmpty().MaximumLength(128);
		}
	}
}
