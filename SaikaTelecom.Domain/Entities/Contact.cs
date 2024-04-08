using SaikaTelecom.Domain.Enum;

namespace SaikaTelecom.Domain.Entities;

public class Contact
{
    public long Id { get; set; }

    public long MarketerId { get; set; }

    public User? Marketer { get; set; }

    public string FirstName { get; set; }

    public string? LastName { get; set; }

    public string? SurName { get; set; }

    public string PhoneNumber { get; set; }

    public string? Email { get; set; }

    public ContactStatus Status {  get; set; }

    public DateTime LastChanged { get; set; }
}
