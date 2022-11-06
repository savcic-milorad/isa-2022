using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransfusionAPI.Application.Identity.Commands.CreateDonor;

namespace TransfusionAPI.WebUI.Controllers;

[AllowAnonymous]
public class IdentityController : ApiControllerBase
{
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

        return Ok(await Task.FromResult("Donor created"));
    }
}
