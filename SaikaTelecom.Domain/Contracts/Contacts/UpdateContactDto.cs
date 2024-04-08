using SaikaTelecom.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace SaikaTelecom.Domain.Contacts;

public class UpdateContactDto
{
    [Required]
    [MaxLength(100)]
    [Display(Name = "First name")]
    public string FirstName { get; set; }

    [MaxLength(100)]
    [Display(Name = "Last name")]
    public string? LastName { get; set; }

    [MaxLength(100)]
    [Display(Name = "Sur name")]
    public string? SurName { get; set; }

    [Phone]
    [Required]
    [MaxLength(100)]
    [Display(Name = "Phone number")]
    public string PhoneNumber { get; set; }

    [EmailAddress]
    [MaxLength(100)]
    [Display(Name = "E-mail address")]
    public string? Email { get; set; }
}
