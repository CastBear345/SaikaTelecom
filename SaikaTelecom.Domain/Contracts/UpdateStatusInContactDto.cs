using SaikaTelecom.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace SaikaTelecom.Domain.Contracts;

public class UpdateStatusInContactDto
{
    [Required]
    [EnumDataType(typeof(ContactStatus))]
    [Display(Name = "Статус контакта")]
    public ContactStatus Status { get; set; }
}
