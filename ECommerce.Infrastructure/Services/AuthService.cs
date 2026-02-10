using ECommerce.Application.DTO.Auth;
using ECommerce.Application.Exceptions;
using ECommerce.Application.Interface;
using ECommerce.Domain.Entities;
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

        // LOGIN 
        public LoginResponseDto Login(LoginDto loginDto)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Username == loginDto.Username);

            if (user == null)
                throw new UnauthorizedException("Invalid username or password");

            if (!user.IsActive)
                throw new UnauthorizedException("User account is inactive");

            var isPasswordValid = _passwordService.VerifyPassword(
                user.PasswordHash,
                loginDto.Password
            );

            if (!isPasswordValid)
                throw new UnauthorizedException("Invalid username or password");

            var token = _jwtService.GenerateToken(user);

            return new LoginResponseDto
            {
                Token = token
            };
        }

        // SIGNUP
        public async Task SignupAsync(SignUpDto dto)
        {
            if (await _context.Users.AnyAsync(x => x.Username == dto.Username))
                throw new AlreadyExistsException("Username already exists");

            if (await _context.Users.AnyAsync(x => x.Email == dto.Email))
                throw new AlreadyExistsException("Email already exists");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                PhoneNo = dto.PhoneNo,
                Username = dto.Username,
                Role = Domain.Enums.UserRole.User,
                IsActive = true
            };

            // Useing the same password service in as login
            user.PasswordHash = _passwordService.HashPassword(dto.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
