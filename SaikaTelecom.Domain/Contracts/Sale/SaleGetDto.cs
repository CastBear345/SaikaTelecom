using System.ComponentModel.DataAnnotations;

namespace SaikaTelecom.Domain.Contracts.Sale;

public class SaleGetDto
{
    [Required]
    [Display(Name = "Id lead")]
    public long LeadId { get; set; }
}
