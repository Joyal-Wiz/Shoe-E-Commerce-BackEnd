using ECommerce.Application.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Interface
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginDto loginDto);

        Task SignupAsync(SignUpDto dto);
        LoginResponseDto RefreshToken(RefreshTokenDto dto);


    }
}
