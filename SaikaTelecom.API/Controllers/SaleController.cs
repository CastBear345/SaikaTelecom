namespace SaikaTelecom.API.Controllers;

[ApiController]
[Route("api/sales")]
public class SaleController : ControllerBase
{
    private readonly SaleService _saleService;

    public SaleController(SaleService saleService)
    {
        _saleService = saleService;
    }

    [Authorize(Roles = $"{nameof(Roles.Admin)},{nameof(Roles.Owner)}")]
    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult<List<SaleResponse>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ForbidResult))]
    public async Task<ActionResult<BaseResult<List<SaleResponse>>>> GetAll()
    {
        var response = await _saleService.GetAllSales();
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [Authorize(Roles = $"{nameof(Roles.Sales)}")]
    [HttpGet("seller-sales")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult<List<SaleResponse>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ForbidResult))]
    public async Task<ActionResult<BaseResult<List<SaleResponse>>>> GetById()
    {
        var response = await _saleService.GetSellerSale();
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [Authorize(Roles = $"{nameof(Roles.Sales)}")]
    [HttpPost("add")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult<SaleResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    public async Task<ActionResult<BaseResult<SaleResponse>>> CreateSale(SaleGetDto dto)
    {
        var response = await _saleService.CreateSale(dto);
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }
}
