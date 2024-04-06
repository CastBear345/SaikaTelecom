using SaikaTelecom.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace SaikaTelecom.Domain.Contracts;

public class UpdateContactDto
{
    [Required]
    [MaxLength(100)]
    [Display(Name = "Имя")]
    public string FirstName { get; set; }

    [MaxLength(100)]
    [Display(Name = "Фамилия")]
    public string? LastName { get; set; }

    [MaxLength(100)]
    [Display(Name = "Отчество")]
    public string? SurName { get; set; }

    [Phone]
    [Required]
    [MaxLength(100)]
    [Display(Name = "Номер телефона")]
    public string PhoneNumber { get; set; }

    [EmailAddress]
    [MaxLength(100)]
    [Display(Name = "Адрес почты")]
    public string? Email { get; set; }
}
