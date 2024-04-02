using SaikaTelecom.Domain.Enum;

namespace SaikaTelecom.Domain.Contracts.UserDtos;

public class UserGetDto
{
    public required long Id { get; init; }
    public required string Email { get; init; }
    public required Roles Role { get; init; }
}
