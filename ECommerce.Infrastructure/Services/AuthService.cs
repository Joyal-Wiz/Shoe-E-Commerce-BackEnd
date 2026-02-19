using ECommerce.Application.DTO.Auth;
using ECommerce.Application.Exceptions;
using ECommerce.Application.Interface;
using ECommerce.Application.Resources;
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

        // LOGIN for both USER and ADMIN
        public LoginResponseDto Login(LoginDto loginDto)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Username == loginDto.Username);

            if (user == null)
                throw new UnauthorizedException(ErrorMessages.InvalidUsername);

            if (!user.IsActive)
                throw new UnauthorizedException(ErrorMessages.UserInactive);

            var isPasswordValid = _passwordService.VerifyPassword(
                user.PasswordHash,
                loginDto.Password
            );

            if (!isPasswordValid)
                throw new UnauthorizedException(ErrorMessages.InvalidPassword);

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
                throw new AlreadyExistsException(ErrorMessages.UsernameAlreadyExists);

            if (await _context.Users.AnyAsync(x => x.Email == dto.Email))
                throw new AlreadyExistsException(ErrorMessages.EmailAlreadyExists);

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

            user.PasswordHash = _passwordService.HashPassword(dto.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }


        // REFRESH TOKEN
        public LoginResponseDto RefreshToken(RefreshTokenDto dto)
        {
            var user = _context.Users
                .FirstOrDefault(x => x.RefreshToken == dto.RefreshToken);

            if (user == null)
                throw new UnauthorizedException(ErrorMessages.RefreshTokenInvalid);

            if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                throw new UnauthorizedException(ErrorMessages.RefreshTokenExpired);

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
