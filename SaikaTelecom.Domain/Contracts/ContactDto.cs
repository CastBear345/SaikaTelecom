using SaikaTelecom.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace SaikaTelecom.Domain.Contracts;

public class ContactDto
{
    [Required]
    [Display(Name = "Идентификатор")]
    public long Id { get; set; }

    [Required]
    [Display(Name = "Идентификатор маркетолога")]
    public long MarketerId { get; set; }

    [Required]
    [MaxLength(50)]
    [Display(Name = "Имя")]
    public string FirstName { get; set; }

    [MaxLength(50)]
    [Display(Name = "Фамилия")]
    public string? LastName { get; set; }

    [MaxLength(50)]
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

    [Required]
    [EnumDataType(typeof(ContactStatus))]
    [Display(Name = "Статус контакта")]
    public ContactStatus Status { get; set; }

    [Required]
    [Display(Name = "Последнее изменение")]
    public DateTime LastChanged { get; set; }
}
