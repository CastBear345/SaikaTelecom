using SaikaTelecom.Domain.Enum;

namespace SaikaTelecom.Domain.Contracts.UserDtos;

public class UserDto
{
    public string FullName { get; set; }

    public string Email { get; set; }

    public Roles Role { get; set; }

    public DateTime? BlockingDate { get; set; }
}
