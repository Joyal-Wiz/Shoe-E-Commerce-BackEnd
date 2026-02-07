using ECommerce.Application.DTO.Auth;
using ECommerce.Application.Interface;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly PasswordService _passwordService;
        private readonly JwtService _jwtService;

        public AuthService(
            AppDbContext context,
            PasswordService passwordService,
            JwtService jwtService)
        {
            _context = context;
            _passwordService = passwordService;
            _jwtService = jwtService;
        }



        public LoginResponseDto Login(LoginDto loginDto)
        {
            // 1. Find user by username
            var user = _context.Users
                .FirstOrDefault(u => u.Username == loginDto.Username);

            if (user == null)
            {
                throw new Exception("Invalid username or password");
            }

            // 2. Check if user is active
            if (!user.IsActive)
            {
                throw new Exception("User account is inactive");
            }

            // 3. Verify password
            var isPasswordValid = _passwordService.VerifyPassword(
                user.PasswordHash,
                loginDto.Password
            );

            if (!isPasswordValid)
            {
                throw new Exception("Invalid username or password");
            }

            // 4. Generate JWT token
            var token = _jwtService.GenerateToken(user);

            return new LoginResponseDto
            {
                Token = token
            };
        }
    }
}
