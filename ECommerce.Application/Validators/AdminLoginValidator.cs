using ECommerce.Application.DTO.Auth;
using ECommerce.Application.Resources;
using FluentValidation;

namespace ECommerce.Application.Validators
{
    public class AdminLoginValidator : AbstractValidator<AdminLoginDto>
    {
        public AdminLoginValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage(ValidationMessages.UsernameRequired)
                .MinimumLength(4).WithMessage(ValidationMessages.UsernameMinLength);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ValidationMessages.PasswordRequired);
                
        }
    }
}
