using ECommerce.Application.DTO.Auth;
using ECommerce.Application.Resources;
using FluentValidation;

public class RefreshTokenDtoValidator : AbstractValidator<RefreshTokenDto>
{
    public RefreshTokenDtoValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage(ErrorMessages.Refreshtokenisrequired);
    }
}