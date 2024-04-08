using SaikaTelecom.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SaikaTelecom.Domain.Contracts.UserDtos;

public class SignUpUserDto
{
    [Required]
    [Display(Name = "Full Name")]
    [MaxLength(100)]
    public string? FullName { get; set; }

    [Required]
    [Display(Name = "E-mail address")]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Display(Name = "Password")]
    [MaxLength(100)]
    [PasswordPropertyText]
    public string Password { get; set; }

    [Required]
    [Display(Name = "User role")]
    [EnumDataType(typeof(Roles))]
    public Roles Role { get; set; }
}
