using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SGE.Application.DTOs.Users;
using SGE.Application.Interfaces.Services;
using SGE.Core.Entities;
using SGE.Core.Exceptions;

namespace SGE.Application.Services;

/// <summary>
/// Provides authentication and authorization services such as 
/// user registration, login, token generation, and token refresh.
/// </summary>
public class AuthService(
    UserManager<ApplicationUser> userManager,
    ITokenService tokenService,
    IMapper mapper
    ) : IAuthService
{
    /// <summary>
    /// Registers a new user with the provided registration details.
    /// </summary>
    /// <param name="registerDto">The user registration data (username, email, password).</param>
    /// <returns>An <see cref="AuthResponseDto"/> containing access and refresh tokens and user information.</returns>
    /// <exception cref="UserAlreadyExistsException">Thrown when the email or username already exists in the system.</exception>
    /// <exception cref="UserRegistrationException">Thrown when user creation fails due to Identity errors.</exception>
    public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        var existingUserByEmail = await userManager.FindByEmailAsync(registerDto.Email);
        if (existingUserByEmail != null)
            throw new UserAlreadyExistsException(registerDto.Email, "email");

        var existingUserByUsername = await userManager.FindByNameAsync(registerDto.UserName);
        if (existingUserByUsername != null)
            throw new UserAlreadyExistsException(registerDto.UserName, "nom d'utilisateur");

        var user = mapper.Map<ApplicationUser>(registerDto);

        var result = await userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            throw new UserRegistrationException(errors);
        }

        await userManager.AddToRoleAsync(user, "User");

        var roles = await userManager.GetRolesAsync(user);
        var accessToken = tokenService.GenerateAccessToken(user, roles);
        var refreshToken = await tokenService.CreateRefreshTokenAsync(user.Id);

        user.LastLoginAt = DateTime.UtcNow;
        await userManager.UpdateAsync(user);

        var userDto = mapper.Map<UserDto>(user);
        userDto.Roles = roles;

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
            ExpiresAt = refreshToken.ExpiresAt,
            User = userDto
        };

    }

    /// <summary>
    /// Authenticates a user with the provided login credentials.
    /// </summary>
    /// <param name="loginDto">The login data (email and password).</param>
    /// <returns>An <see cref="AuthResponseDto"/> containing access and refresh tokens and user information.</returns>
    /// <exception cref="InvalidCredentialsException">Thrown when the email or password is incorrect.</exception>
    /// <exception cref="UserNotActiveException">Thrown when the user account is disabled or inactive.</exception>
    public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        var user = await userManager.FindByEmailAsync(loginDto.Email);
        if (user == null || !await userManager.CheckPasswordAsync(user, loginDto.Password))
            throw new InvalidCredentialsException();

        if (!user.IsActive)
            throw new UserNotActiveException();

        var roles = await userManager.GetRolesAsync(user);
        var accessToken = tokenService.GenerateAccessToken(user, roles);
        var refreshToken = await tokenService.CreateRefreshTokenAsync(user.Id);

        user.LastLoginAt = DateTime.UtcNow;
        await userManager.UpdateAsync(user);

        var userDto = mapper.Map<UserDto>(user);
        userDto.Roles = roles;

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
            ExpiresAt = refreshToken.ExpiresAt,
            User = userDto
        };
    }
    
    /// <summary>
    /// Refreshes the access token using a valid refresh token.
    /// </summary>
    /// <param name="refreshTokenDto">The refresh token data (access token and refresh token).</param>
    /// <returns>An <see cref="AuthResponseDto"/> containing new access and refresh tokens and user information.</returns>
    /// <exception cref="InvalidRefreshTokenException">Thrown when the refresh token is invalid or expired.</exception>
    /// <exception cref="UserNotActiveException">Thrown when the user account is disabled or inactive.</exception>
    public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
    {
        var principal = tokenService.GetPrincipalFromExpiredToken(refreshTokenDto.AccessToken);
        var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            throw new InvalidRefreshTokenException();
        
        var isValidRefreshToken = await tokenService.ValidateRefreshTokenAsync(refreshTokenDto.RefreshToken, userId);
        if (!isValidRefreshToken)
            throw new InvalidRefreshTokenException();

        var user = await userManager.FindByIdAsync(userId);
        if (user is not { IsActive: true })
            throw new UserNotActiveException();
        
        await tokenService.RevokeRefreshTokenAsync(refreshTokenDto.RefreshToken, "Remplacé par un nouveau token");
        
        var roles = await userManager.GetRolesAsync(user);
        var newAccessToken = tokenService.GenerateAccessToken(user, roles);
        var newRefreshToken = await tokenService.CreateRefreshTokenAsync(user.Id);
        
        user.LastLoginAt = DateTime.UtcNow;
        await userManager.UpdateAsync(user);

        var userDto = mapper.Map<UserDto>(user);
        userDto.Roles = roles;

        return new AuthResponseDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken.Token,
            ExpiresAt = newRefreshToken.ExpiresAt,
            User = userDto
        };

    }

    /// <summary>
    /// Logs out a user by revoking all their active refresh tokens.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns><c>true</c> if logout succeeded, otherwise <c>false</c>.</returns>
    public async Task<bool> LogoutAsync(string userId)
    {
        await tokenService.RevokeAllUserRefreshTokensAsync(userId);
        return true;
    }

    /// <summary>
    /// Revokes a specific refresh token.
    /// </summary>
    /// <param name="token">The refresh token string to revoke.</param>
    /// <returns><c>true</c> if the token was revoked successfully, otherwise <c>false</c>.</returns>
    public async Task<bool> RevokeTokenAsync(string token)
    {
        await tokenService.RevokeRefreshTokenAsync(token, "Révoqué par l'utilisateur");
        return true;
    }

    /// <summary>
    /// Retrieves the details of the currently authenticated user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>A <see cref="UserDto"/> representing the user, or <c>null</c> if not found.</returns>
    public async Task<UserDto?> GetCurrentUserAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            return null;
        
        var roles = await userManager.GetRolesAsync(user);
        var userDto = mapper.Map<UserDto>(user);
        userDto.Roles = roles;
        
        return userDto;
    }
}