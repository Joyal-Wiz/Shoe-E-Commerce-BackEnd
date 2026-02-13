using ECommerce.Application.DTO.Auth;
using FluentValidation;

namespace ECommerce.Application.Validators
{
    public class AdminLoginValidator : AbstractValidator<AdminLoginDto>
    {
        public AdminLoginValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Admin username is required")
                .MinimumLength(4).WithMessage("Admin username must be at least 4 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Admin password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");
        }
    }
}
