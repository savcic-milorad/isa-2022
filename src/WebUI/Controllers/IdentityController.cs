using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransfusionAPI.Application.Identity.Commands.CreateDonor;
using TransfusionAPI.Application.Identity.Queries.GetApplicationUser;

namespace TransfusionAPI.WebUI.Controllers;

[AllowAnonymous]
public class IdentityController : ApiControllerBase
{

    //[Authorize(Roles = SupportedRoles.Administrator)]
    [HttpGet("ApplicationUsers/{ApplicationUserId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApplicationUserDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetApplicationUser([FromRoute] string applicationUserId)
    {
        var query = new GetApplicationUserQuery() { ApplicationUserId = applicationUserId };

        var result = await Mediator.Send(query);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(result.Payload);
    }

    [HttpPost("Donors")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreatedDonorDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterDonor(CreateDonorCommand createDonorCommand)
    {
        var result = await Mediator.Send(createDonorCommand);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return CreatedAtAction(nameof(DonorController.GetDonor), "Donor", new { donorId = result.Payload.Id }, result.Payload);
    }
}
