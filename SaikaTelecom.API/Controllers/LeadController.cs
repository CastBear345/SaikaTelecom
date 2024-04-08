namespace SaikaTelecom.API.Controllers;

[ApiController]
[Route("api/lead")]
public class LeadController : ControllerBase
{
    private readonly LeadService _leadService;

    public LeadController(LeadService leadService)
    {
        _leadService = leadService;
    }

    [HttpGet("{sellerId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult<LeadResponse>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ForbidResult))]
    public async Task<ActionResult<BaseResult<LeadResponse>>> GetSellerLeads(long sellerId)
    {
        var response = await _leadService.GetSellerLeads(sellerId);
        if (response.IsSuccess)
            return Ok(response.Data);

        return BadRequest(response.ErrorMessage);
    }

    [HttpPost("add")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult<LeadResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ForbidResult))]
    public async Task<ActionResult<BaseResult<LeadResponse>>> CreateLead(LeadCreateDto dto)
    {
        var response = await _leadService.CreateLead(dto);
        if (response.IsSuccess)
            return Ok(response.Data);

        return BadRequest(response.ErrorMessage);
    }

    [HttpPut("status/{leadId}/{newStatus}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult<LeadResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ForbidResult))]
    public async Task<ActionResult<BaseResult<LeadResponse>>> ChangeLeadStatus(long leadId, LeadStatus newStatus)
    {
        var response = await _leadService.ChangeLeadStatus(leadId, newStatus);
        if (response.IsSuccess)
            return Ok(response.Data);
        
        return BadRequest(response.ErrorMessage);
    }
}