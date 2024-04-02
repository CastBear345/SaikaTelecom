using Microsoft.AspNetCore.Mvc;

namespace SaikaTelecom.API.Controllers;

[ApiController]
[Route("api/contact")]
public class ContactController : ControllerBase
{
    [HttpGet]
    public IActionResult Test()
    {
        return Ok();
    }
}
