namespace SaikaTelecom.Domain.Entities;

public class Sale
{
    public long Id { get; set; }

    public long LeadId { get; set; }

    public long SellerId { get; set; }

    public DateTime DateOfSale { get; set; }
}
