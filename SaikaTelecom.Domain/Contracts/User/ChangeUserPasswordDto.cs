using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SaikaTelecom.Domain.Contracts.UserDtos;

public record ChangeUserPasswordDto
{
    [Required]
    [Display(Name = "Old Password")]
    [MaxLength(100)]
    [PasswordPropertyText]
    public string OldPassword { get; set; }

    [Required]
    [Display(Name = "New Password")]
    [MaxLength(100)]
    [PasswordPropertyText]
    public string NewPassword { get; set; }
}
