using ECommerce.Application.DTO.Auth;
using ECommerce.Application.Resources;
using FluentValidation;

namespace ECommerce.Application.Validators
{
    public class SignUpDtoValidator : AbstractValidator<SignUpDto>
    {
        public SignUpDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationMessages.NameRequired)
                .MinimumLength(3).WithMessage(ValidationMessages.NameMinLength);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(ValidationMessages.EmailRequired)
                .EmailAddress().WithMessage(ValidationMessages.InvalidEmail);

            RuleFor(x => x.PhoneNo)
                .NotEmpty().WithMessage(ValidationMessages.PhoneRequired)
                .Matches(@"^[0-9]{10}$")
                .WithMessage(ValidationMessages.InvalidPhone);

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage(ValidationMessages.UsernameRequired)
                .MinimumLength(4).WithMessage(ValidationMessages.UsernameMinLength)
                .Matches(@"^[a-zA-Z0-9_]*$")
                .WithMessage(ValidationMessages.UsernameInvalid);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ValidationMessages.PasswordRequired)
                .MinimumLength(6).WithMessage(ValidationMessages.PasswordMinLength)
                .Matches(@"[A-Z]").WithMessage(ValidationMessages.PasswordUppercase)
                .Matches(@"[a-z]").WithMessage(ValidationMessages.PasswordLowercase)
                .Matches(@"[0-9]").WithMessage(ValidationMessages.PasswordDigit);
        }
    }
}
