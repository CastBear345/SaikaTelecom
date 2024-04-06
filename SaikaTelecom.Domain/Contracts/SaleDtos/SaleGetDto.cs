﻿using System.ComponentModel.DataAnnotations;

namespace SaikaTelecom.Domain.Contracts.SaleDtos;

public class SaleGetDto
{
    [Required]
    [Display(Name = "Идентификатор лида")]
    public long LeadId { get; set; }

    [Required]
    [Display(Name = "Идентификатор продавца")]
    public long SellerId { get; set; }
}
