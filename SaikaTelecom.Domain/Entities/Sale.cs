namespace SaikaTelecom.Domain.Entities;

public class Sale
{
    public long Id { get; set; }

    public long LeadId { get; set; }

    public Lead Lead { get; set; }

    public long SellerId { get; set; }

    public User Seller { get; set; }

    public DateTime DateOfSale { get; set; }
}
