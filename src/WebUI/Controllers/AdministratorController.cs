using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransfusionAPI.Application.Identity.Queries.GetApplicationUser;
using TransfusionAPI.Domain.Constants;

namespace TransfusionAPI.WebUI.Controllers;

[Authorize(Roles = SupportedRoles.Administrator)]
public class AdministratorController : ApiControllerBase
{
    [HttpGet("SayHello")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApplicationUserDto))]
    public async Task<IActionResult> SayHelloToAdministrator()
    {
        return Ok("Hello administrator");
    }
}
