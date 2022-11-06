using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransfusionAPI.Application.Common.Constants;
using TransfusionAPI.Application.Identity.Commands.CreateDonor;

namespace TransfusionAPI.WebUI.Controllers;

[AllowAnonymous]
public class IdentityController : ApiControllerBase
{
    //[Authorize(Roles = SupportedRoles.Administrator)]
    [HttpGet("ApplicationUsers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUsers(CreateDonorCommand createDonorCommand)
    {

    }

    [HttpPost("Donors")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterDonor(CreateDonorCommand createDonorCommand)
    {
        var result = await Mediator.Send(createDonorCommand);

        if(!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return CreatedAtAction(DonorController.);
    }
}
