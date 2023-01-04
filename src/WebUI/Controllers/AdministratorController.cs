using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransfusionAPI.Domain.Constants;

namespace TransfusionAPI.WebUI.Controllers;

[Authorize(Roles = SupportedRoles.Administrator)]
public class AdministratorController : ApiControllerBase
{
    [HttpGet("SayHello")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    public async Task<IActionResult> SayHelloToAdministrator()
    {
        return Ok(await Task.FromResult("Hello administrator"));
    }
}
