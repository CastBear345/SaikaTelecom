﻿using SaikaTelecom.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace SaikaTelecom.Domain.Contacts;

public class UpdateStatusInContactDto
{
    [Required]
    [EnumDataType(typeof(ContactStatus))]
    [Display(Name = "Contact status")]
    public ContactStatus Status { get; set; }
}
