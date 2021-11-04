using FluentValidation;
using SimpleMailService___SMS.Domain.Commands;

namespace SimpleMailService___SMS.Domain.Validations
{
	public class SendEmailComandValidation : AbstractValidator<SendEmailComand>
	{
		public SendEmailComandValidation()
		{
			RuleFor(x => x.From).NotNull().NotEmpty().WithMessage("Email FROM is required.");
			RuleFor(x => x.To).NotNull().NotEmpty().WithMessage("Email TO is required.");

			RuleFor(x => x.From).EmailAddress().WithMessage("Invalid email address.");
			RuleFor(x => x.To).EmailAddress().WithMessage("Invalid email address.");
		}
	}
}
