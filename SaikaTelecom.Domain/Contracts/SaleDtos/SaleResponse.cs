using System.ComponentModel.DataAnnotations;

namespace SaikaTelecom.Domain.Contracts.SaleDtos;

public record SaleResponse
{
    [Required]
    [Display(Name = "Идентификатор лида")]
    public long LeadId { get; set; }

    [Required]
    [Display(Name = "Идентификатор продавца")]
    public long SellerId { get; set; }

    [Required]
    [Display(Name = "Дата продажи")]
    public DateTime DateOfSale { get; set; }
}
