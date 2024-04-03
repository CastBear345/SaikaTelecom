using Microsoft.AspNetCore.Mvc;
using SaikaTelecom.Application.Services;
using SaikaTelecom.Domain.Contracts.SaleDtos;

namespace SaikaTelecom.API.Controllers;

[ApiController]
[Route("api/sale/")]
public class SaleController : ControllerBase
{
    private readonly SaleService _saleService;

    public SaleController(SaleService saleService)
    {
        _saleService = saleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var sales = await _saleService.GetAllSales();

        return Ok(sales);
    }

    [HttpGet("{sellerId}")]
    public async Task<IActionResult> GetById(long sellerId)
    {
        var sales = await _saleService.GetBySellerId(sellerId);

        return Ok(sales);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSale(SaleGetDto dto)
    {
        await _saleService.CreateSale(dto);

        return NoContent();
    }
}
