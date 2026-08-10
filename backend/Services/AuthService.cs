using System.Security.Cryptography;
using TaskPilot.Api.DTOs.Auth;
using TaskPilot.Api.Interfaces;
using TaskPilot.Api.Models;

namespace TaskPilot.Api.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public AuthService(
        IUserRepository userRepository,
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        var user = await _userRepository
            .GetByUsernameOrEmailAsync(dto.UsernameOrEmail);

        if (user == null)
            throw new UnauthorizedAccessException("Invalid credentials.");

        if (user.PasswordHash == null)
            throw new UnauthorizedAccessException(
                "This account does not use password authentication.");

        var passwordValid = BCrypt.Net.BCrypt.Verify(
            dto.Password,
            user.PasswordHash);

        if (!passwordValid)
            throw new UnauthorizedAccessException("Invalid credentials.");

        var accessToken = _jwtService.GenerateAccessToken(user);

        var refreshToken = GenerateRefreshToken();

        user.RefreshTokens.Add(refreshToken);

        await _userRepository.UpdateAsync(user);

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token
        };
    }

    public async Task<AuthResponseDto> RefreshAsync(string refreshToken)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
            throw new UnauthorizedAccessException("Refresh token is required.");

        var user = await _userRepository
            .GetByRefreshTokenAsync(refreshToken);

        if (user == null)
            throw new UnauthorizedAccessException("Invalid refresh token.");

        var token = user.RefreshTokens
            .FirstOrDefault(x => x.Token == refreshToken);

        if (token == null ||
            token.IsRevoked ||
            token.ExpiresAt <= DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException(
                "Refresh token is invalid or expired.");
        }

        var newAccessToken = _jwtService.GenerateAccessToken(user);

        return new AuthResponseDto
        {
            AccessToken = newAccessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task RegisterAsync(RegisterDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        if (await _userRepository.ExistsByUsernameAsync(dto.Username))
            throw new InvalidOperationException("Username is already taken.");

        if (await _userRepository.ExistsByEmailAsync(dto.Email))
            throw new InvalidOperationException("Email is already registered.");

        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        await _userRepository.CreateAsync(user);
    }

    private RefreshToken GenerateRefreshToken()
    {
        return new RefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };
    }
}