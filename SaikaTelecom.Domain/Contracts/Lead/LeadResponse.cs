using SaikaTelecom.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace SaikaTelecom.Domain.Contracts.Lead;

public record LeadResponse
{
    [Required]
    [Display(Name = "Идентификатор контакта")]
    public long ContactId { get; set; }

    [Required]
    [Display(Name = "Идентификатор продавца")]
    public long? SellerId { get; set; }

    [Required]
    [Display(Name = "Статус лида")]
    [EnumDataType(typeof(LeadStatus))]
    public LeadStatus LeadStatus { get; set; }
}
