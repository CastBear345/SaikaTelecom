using SaikaTelecom.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace SaikaTelecom.Domain.Contracts.UserDtos;

public class UserGetDto
{
    [Required]
    [Display(Name = "Id")]
    public required long Id { get; init; }

    [Required]
    [Display(Name = "E-mail address")]
    [MaxLength(100)]
    [EmailAddress]
    public required string Email { get; init; }

    [Required]
    [Display(Name = "User role")]
    [EnumDataType(typeof(Roles))]
    public required Roles Role { get; init; }
}
