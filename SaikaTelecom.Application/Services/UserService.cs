using SaikaTelecom.DAL;
using SaikaTelecom.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using SaikaTelecom.Domain.Contracts.UserDtos;
using AutoMapper;
using SaikaTelecom.Domain.Enum;
using FonTech.Domain.Result;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace SaikaTelecom.Application.Services;

public class UserService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly HttpContext _httpContext;
    private readonly IMapper _mapper;

    public UserService(ApplicationDbContext dbContext, IHttpContextAccessor accessor, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _httpContext = accessor.HttpContext ?? throw new ArgumentException(nameof(accessor.HttpContext));
    }

    public async Task<BaseResult> SignInAsync(LogInUserDto dto)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user == null) return new BaseResult() { ErrorMessage = "Invalid email." };

        if (user.BlockingDate != null) return new BaseResult() { ErrorMessage = "User is blocked" };

        if (!string.IsNullOrEmpty(dto.Password) && BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        {
            var userGetDto = _mapper.Map<UserGetDto>(user);
            await SignInWithHttpContext(userGetDto);
            return new BaseResult();
        }

        return new BaseResult() { ErrorMessage = "Invalid password." };
    }

    public async Task<BaseResult> SignUpAsync(RegisterUserDto dto)
    {
        var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (existingUser != null) return new BaseResult() { ErrorMessage = "User with this email already exists." };

        if (string.IsNullOrEmpty(dto.Password)) return new BaseResult() { ErrorMessage = "Invalid password." };

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

        return new BaseResult<RegisterUserDto>() { Data = dto };
    }

    public async Task<BaseResult<List<UserDto>>> GetAllUsers()
    {
        var users = await _dbContext.Users.ToListAsync();
        if (users == null) return new BaseResult<List<UserDto>>() { ErrorMessage = "Users not found." };

        var userDtos = _mapper.Map<List<UserDto>>(users);
        return new BaseResult<List<UserDto>> { Data = userDtos };
    }

    public async Task<BaseResult<UserDto>> GetUser(long userId)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) return new BaseResult<UserDto>() { ErrorMessage = "User not found." };

        var userDto = _mapper.Map<UserDto>(user);
        return new BaseResult<UserDto> { Data = userDto };
    }

    public async Task<BaseResult> BlockUser(long userId)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) return new BaseResult() { ErrorMessage = "User not found." };

        user.BlockingDate = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();
        return new BaseResult();
    }

    public async Task<BaseResult> UnblockUser(long userId)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) return new BaseResult() { ErrorMessage = "User not found." };

        user.BlockingDate = null;
        await _dbContext.SaveChangesAsync();
        return new BaseResult();
    }

    public async Task<BaseResult> DeleteUser(long userId)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) return new BaseResult() { ErrorMessage = "User not found." };

        if (user.Role != Roles.Admin || _httpContext.User.IsInRole(nameof(Roles.Owner))) {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return new BaseResult();
        }

        return new BaseResult() { ErrorMessage = "You can't remove the admin." };
    }

    public async Task<BaseResult<UserDto>> ChangeUserRole(long userId, Roles newRole)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) return new BaseResult<UserDto>() { ErrorMessage = "User not found." };

        if (user.Role == newRole) return new BaseResult<UserDto>() { ErrorMessage = "The user already has this role." };

        user.Role = newRole;
        await _dbContext.SaveChangesAsync();
        return new BaseResult<UserDto>();
    }

    public async Task<BaseResult> ChangePassword(long userId, string newPassword)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) return new BaseResult() { ErrorMessage = "User not found." };

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
        user.PasswordHash = passwordHash;
        await _dbContext.SaveChangesAsync();
        return new BaseResult();
    }

    public async Task<BaseResult> LogOut()
    {
        await _httpContext.SignOutAsync();
        return new BaseResult();
    }

    private Task SignInWithHttpContext(UserGetDto userDto)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
            new(ClaimTypes.Name, userDto.Email),
            new(ClaimTypes.Email, userDto.Email),
            new(ClaimTypes.Role, nameof(userDto.Role)),
        };

        var claimsIdentity = new ClaimsIdentity(claims, "cookie");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        return _httpContext.SignInAsync(claimsPrincipal);
    }
}
