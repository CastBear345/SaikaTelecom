using SaikaTelecom.Domain.Enum;

namespace SaikaTelecom.Domain.Entities;

public class Contact
{
    public required long Id { get; set; }

    public required long MarketerId { get; set; }

    public required string FirstName { get; set; }

    public string? LastName { get; set; }

    public string? SurName { get; set; }

    public required string PhoneNumber { get; set; }

    public string? Email { get; set; }

    public required Status Status {  get; set; }

    public required DateTime LastChanged { get; set; }
}
