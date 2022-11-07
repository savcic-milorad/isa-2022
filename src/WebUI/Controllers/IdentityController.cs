using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransfusionAPI.Application.Identity.Commands.CreateDonor;
using TransfusionAPI.Application.Identity.Queries.GetApplicationUser;
using TransfusionAPI.Domain.Entities;

namespace TransfusionAPI.WebUI.Controllers;

[AllowAnonymous]
public class IdentityController : ApiControllerBase
{

    //[Authorize(Roles = SupportedRoles.Administrator)]
    [HttpGet("ApplicationUsers/{ApplicationUserId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApplicationUser))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUser([FromRoute] string applicationUserId)
    {
        var query = new GetApplicationUserQuery() { ApplicationUserId = applicationUserId };

        var result = await Mediator.Send(query);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(result.Payload);
    }

    [HttpPost("Donors")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterDonor(CreateDonorCommand createDonorCommand)
    {
        var result = await Mediator.Send(createDonorCommand);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return CreatedAtAction(nameof(GetUser), "Identity", new { applicationUserId = result.Payload.ApplicationUserId }, result.Payload);
    }
}
