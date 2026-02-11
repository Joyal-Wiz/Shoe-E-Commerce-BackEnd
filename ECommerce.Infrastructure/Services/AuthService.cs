using ECommerce.Application.DTO.Auth;
using ECommerce.Application.Exceptions;
using ECommerce.Application.Interface;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
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

        // USER LOGIN 
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
            var refreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            _context.SaveChanges();

            return new LoginResponseDto
            {
                Token = token,
                RefreshToken = refreshToken
            };

        }

        // USER SIGNUP
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
                Role = UserRole.User,
                IsActive = true
            };

            // Using same password service as login
            user.PasswordHash = _passwordService.HashPassword(dto.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        // ADMIN LOGIN
        public LoginResponseDto AdminLogin(AdminLoginDto dto)
        {
            var admin = _context.Users
                .FirstOrDefault(x => x.Username == dto.Username
                                  && x.Role == UserRole.Admin);

            if (admin == null)
                throw new UnauthorizedException("Invalid admin credentials");

            if (!admin.IsActive)
                throw new UnauthorizedException("Admin account is inactive");

            var isPasswordValid = _passwordService.VerifyPassword(
                admin.PasswordHash,
                dto.Password
            );

            if (!isPasswordValid)
                throw new UnauthorizedException("Invalid admin credentials");

            var token = _jwtService.GenerateToken(admin);
            var refreshToken = _jwtService.GenerateRefreshToken();

            admin.RefreshToken = refreshToken;
            admin.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            _context.SaveChanges();

            return new LoginResponseDto
            {
                Token = token,
                RefreshToken = refreshToken
            };

        }
        public LoginResponseDto RefreshToken(RefreshTokenDto dto)
        {
            var user = _context.Users
                .FirstOrDefault(x => x.RefreshToken == dto.RefreshToken);

            if (user == null)
                throw new UnauthorizedException("Invalid refresh token");

            if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                throw new UnauthorizedException("Refresh token expired");

            var newAccessToken = _jwtService.GenerateToken(user);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            _context.SaveChanges();

            return new LoginResponseDto
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

    }
}
