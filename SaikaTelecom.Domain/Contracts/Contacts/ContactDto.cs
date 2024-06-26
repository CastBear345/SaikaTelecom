﻿using SaikaTelecom.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace SaikaTelecom.Domain.Contacts;

public class ContactDto
{
    [Required]
    [Display(Name = "Id")]
    public long Id { get; set; }

    [Required]
    [Display(Name = "Id marketer")]
    public long MarketerId { get; set; }

    [Required]
    [MaxLength(50)]
    [Display(Name = "First name")]
    public string FirstName { get; set; }

    [MaxLength(50)]
    [Display(Name = "Last name")]
    public string? LastName { get; set; }

    [MaxLength(50)]
    [Display(Name = "Sur name")]
    public string? SurName { get; set; }

    [Phone]
    [Required]
    [MaxLength(100)]
    [Display(Name = "Pgone number")]
    public string PhoneNumber { get; set; }

    [EmailAddress]
    [MaxLength(100)]
    [Display(Name = "E-mail address")]
    public string? Email { get; set; }

    [Required]
    [EnumDataType(typeof(ContactStatus))]
    [Display(Name = "Contact status")]
    public ContactStatus Status { get; set; }

    [Required]
    [Display(Name = "Last changed")]
    public DateTime LastChanged { get; set; }
}
