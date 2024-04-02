using SaikaTelecom.Domain.Enum;

namespace SaikaTelecom.Domain.Contracts.UserDtos;

public class RegisterUserDto
{
    public string? FullName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public Roles Role { get; set; }
}
