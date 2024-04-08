namespace SaikaTelecom.API.Controllers;

[ApiController]
[Route("api/contacts")]
public class ContactController : ControllerBase
{
    private readonly ContactService _contactService;

    public ContactController(ContactService contactService)
    {
        _contactService = contactService;
    }

    [Authorize(Roles = $"{nameof(Roles.Admin)},{nameof(Roles.Owner)},{nameof(Roles.Marketing)}")]
    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult<List<ContactDto>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ForbidResult))]
    public async Task<ActionResult<BaseResult<List<ContactDto>>>> GetAllContacts()
    {
        var response = await _contactService.GetAllContacts();
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [Authorize(Roles = $"{nameof(Roles.Sales)}")]
    [HttpGet("lead")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult<List<ContactDto>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ForbidResult))]
    public async Task<ActionResult<BaseResult<List<ContactDto>>>> GetLeadContacts()
    {
        var response = await _contactService.GetLeadContacts();
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [Authorize(Roles = $"{nameof(Roles.Marketing)}")]
    [HttpPost("add")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult<ContactDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    public async Task<ActionResult<BaseResult<ContactDto>>> CreateContact(CreateContactDto dto)
    {
        var response = await _contactService.CreateContact(dto);
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [Authorize(Roles = $"{nameof(Roles.Sales)},{nameof(Roles.Marketing)}")]
    [HttpPut("update/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult<ContactDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ForbidResult))]
    public async Task<ActionResult<BaseResult<ContactDto>>> UpdateContact(long id, UpdateContactDto dto)
    {
        var response = await _contactService.UpdateContact(id, dto);
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [Authorize(Roles = $"{nameof(Roles.Marketing)}")]
    [HttpPut("change-status/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult<ContactDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ForbidResult))]
    public async Task<ActionResult<BaseResult<ContactDto>>> ChangeContactStatus(long id, UpdateStatusInContactDto dto)
    {
        var response = await _contactService.ChangeContactStatus(id, dto);
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }
}
