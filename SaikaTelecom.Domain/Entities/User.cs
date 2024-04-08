using SaikaTelecom.Domain.Enum;

namespace SaikaTelecom.Domain.Entities;

public class User
{
    public long Id { get; set; }

    public string? FullName { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public Roles Role { get; set; }

    public DateTime? BlockingDate { get; set; }
}
