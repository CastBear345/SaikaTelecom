using FonTech.Domain.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaikaTelecom.Application.Services;
using SaikaTelecom.Domain.Contracts.UserDtos;
using SaikaTelecom.Domain.Entities;
using SaikaTelecom.Domain.Enum;

namespace SaikaTelecom.API.Controllers;

[Authorize]
[ApiController]
[Route("api/user/")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("sign-in")]
    public async Task<ActionResult<BaseResult>> SignIn(LogInUserDto dto)
    {
        var response = await _userService.SignInAsync(dto);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("sign-up")]
    public async Task<ActionResult<BaseResult>> SignUp(RegisterUserDto dto)
    {
        var response = await _userService.SignUpAsync(dto);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("users")]
    public async Task<ActionResult<BaseResult<List<UserDto>>>> GetAllUsers()
    {
        var response = await _userService.GetAllUsers();

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<BaseResult<UserDto>>> GetUser(long userId)
    {
        var response = await _userService.GetUser(userId);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("block-user/{userId}")]
    public async Task<ActionResult<BaseResult>> BlockUser(long userId)
    {
        var response = await _userService.BlockUser(userId);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("unblock-user/{userId}")]
    public async Task<ActionResult<BaseResult>> UnblockUser(long userId)
    {
        var response = await _userService.UnblockUser(userId);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("user/{userId}")]
    public async Task<ActionResult<BaseResult>> DeleteUser(long userId)
    {
        var response = await _userService.DeleteUser(userId);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("role/{userId}")]
    public async Task<ActionResult<BaseResult<UserDto>>> ChangeUserRole(long userId, Roles newRole)
    {
        var response = await _userService.ChangeUserRole(userId, newRole);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpPost("password/{userId}")]
    public async Task<ActionResult<BaseResult>> ChangePassword(long userId, string newPassword)
    {
        var response = await _userService.ChangePassword(userId, newPassword);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpPost("log-out")]
    public async Task<ActionResult<BaseResult>> LogOut()
    {
        var response = await _userService.LogOut();

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}
