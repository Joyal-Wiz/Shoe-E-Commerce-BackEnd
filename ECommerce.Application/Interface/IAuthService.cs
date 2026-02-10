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
        LoginResponseDto Login(LoginDto loginDto);
        Task SignupAsync(SignUpDto dto);
    }
}
