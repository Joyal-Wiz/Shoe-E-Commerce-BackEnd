using FluentValidation;
using ECommerce.Application.DTO.Auth;

namespace ECommerce.Application.Validators
{
    public class AdminLoginValidator : AbstractValidator<AdminLoginDto>
    {
        public AdminLoginValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Admin username is required");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Admin password is required");
        }
    }
}
