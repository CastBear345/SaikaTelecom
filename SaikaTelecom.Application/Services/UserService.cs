using SaikaTelecom.DAL;
using SaikaTelecom.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;
using SaikaTelecom.Domain.Contracts.UserDtos;

namespace SaikaTelecom.Application.Services;

public class UserService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly HttpContext _httpContext;

    public UserService(ApplicationDbContext dbContext, IHttpContextAccessor accessor)
    {
        _dbContext = dbContext;

        if (accessor.HttpContext is null)
        {
            throw new ArgumentException(nameof(accessor.HttpContext));
        }
        _httpContext = accessor.HttpContext;
    }
    public async Task LogInAsync(LogInUserDto dto)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user != null)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            if (!string.IsNullOrEmpty(passwordHash) && passwordHash == user.PasswordHash)
            {
                UserGetDto userGetDto = new UserGetDto()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = user.Role,
                };
                await SignInWithHttpContext(userGetDto);
            }
        }
    }

    public async Task RegisterAsync(RegisterUserDto dto)
    {
        var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (existingUser != null)
        {
            throw new InvalidOperationException("User with this email already exists.");
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = passwordHash,
            Role = dto.Role
        };
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<UserDto>> GetUsers()
    {
        var users = _dbContext.Users.ToList();
        var userDtos = users.Select(u => new UserDto
        {
            Email = u.Email,
            FullName = u.FullName
        }).ToList();
        return userDtos;
    }

    public async Task<User> GetUser(long userId)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        return user == null ? throw new KeyNotFoundException("User not found") : user;
    }

    public async Task BlockUser(long userId)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
            throw new KeyNotFoundException("User not found");

        var blockTime = DateTime.UtcNow.ToLongDateString

        var result = await .SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
        return result;
    }

    //public async Task UnblockUser(string userId)
    //{
    //    var user = await .FindByIdAsync(userId);
    //    if (user == null)
    //        throw new KeyNotFoundException("User not found");

    //    var result = await ;
    //    return result;
    //}

    //public async Task DeleteUser(string userId)
    //{
    //    var user = await .FindByIdAsync(userId);
    //    if (user == null)
    //        throw new KeyNotFoundException("User not found");

    //    var result = await .DeleteAsync(user);
    //    return result;
    //}

    //public async Task ChangeUserRole(string userId, string newRole)
    //{
    //    var user = await .FindByIdAsync(userId);
    //    if (user == null)
    //        throw new KeyNotFoundException("User not found");

    //    var currentRoles = await .GetRolesAsync(user);
    //    var result = await .RemoveFromRolesAsync(user, currentRoles);
    //    if (result.Succeeded)
    //    {
    //        result = await .AddToRoleAsync(user, newRole);
    //    }
    //    return result;
    //}

    //public async Task ChangePassword(string userId, string currentPassword, string newPassword)
    //{
    //    var user = await .FindByIdAsync(userId);
    //    if (user == null)
    //        throw new KeyNotFoundException("User not found");

    //    var result = await .ChangePasswordAsync(user, currentPassword, newPassword);
    //    return result;
    //}

    public async Task LogOut()
    {
       await _httpContext.SignOutAsync();
    }

    private Task SignInWithHttpContext(UserGetDto userDto)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
            new(ClaimTypes.Email, userDto.Email),
            new(ClaimTypes.Role, userDto.Role.ToString()),
        };

        var claimsIdentity = new ClaimsIdentity(claims, "cookie");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        return _httpContext.SignInAsync(claimsPrincipal);
    }
}