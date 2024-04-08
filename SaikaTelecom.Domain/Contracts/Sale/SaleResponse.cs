using System.ComponentModel.DataAnnotations;

namespace SaikaTelecom.Domain.Contracts.Sale;

public record SaleResponse
{
    [Required]
    [Display(Name = "Id lead")]
    public long LeadId { get; set; }

    [Required]
    [Display(Name = "Id seller")]
    public long SellerId { get; set; }

    [Required]
    [Display(Name = "Date of sale")]
    public DateTime DateOfSale { get; set; }
}
