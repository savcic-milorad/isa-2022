using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransfusionAPI.Domain.Constants;

namespace TransfusionAPI.WebUI.Controllers;

[Authorize(Roles = SupportedRoles.Staff)]
public class StaffController : ApiControllerBase
{
    [HttpGet("SayHello")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    public async Task<IActionResult> SayHelloToStaff()
    {
        return Ok(await Task.FromResult("Hello staff"));
    }
}
