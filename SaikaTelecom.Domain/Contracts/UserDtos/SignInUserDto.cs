using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SaikaTelecom.Domain.Contracts.UserDtos;

public class SignInUserDto
{
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
}
