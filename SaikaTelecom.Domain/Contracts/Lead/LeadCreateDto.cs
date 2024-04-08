﻿using SaikaTelecom.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace SaikaTelecom.Domain.Contracts.Lead;

public record LeadCreateDto
{
    [Required]
    [Display(Name = "Id contact")]
    public long ContactId { get; set; }

    [Required]
    [Display(Name = "Id seller")]
    public long? SellerId { get; set; }
}
