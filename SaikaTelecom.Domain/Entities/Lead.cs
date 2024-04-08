using SaikaTelecom.Domain.Enum;

namespace SaikaTelecom.Domain.Entities;

public class Lead
{
    public long Id { get; set; }

    public long ContactId { get; set; }

    public Contact Contact { get; set; }

    public long? SellerId { get; set; }

    public User? Seller { get; set; }

    public LeadStatus LeadStatus { get; set; }
}
