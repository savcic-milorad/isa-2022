using Microsoft.AspNetCore.Mvc;
using TransfusionAPI.Application.Common.Constants;
using TransfusionAPI.Application.Common.Security;

namespace TransfusionAPI.WebUI.Controllers;

[Authorize(Roles = SupportedRoles.Donor)]
public class DonorController : ApiControllerBase
{

    [HttpGet("{donorId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSingle([FromRoute(Name = "donorId")] int donorId)
    {
        if(donorId < 0)
            return BadRequest();
        else if(donorId > 1)
            return NotFound();

        return Ok(await Task.FromResult(new { Id = donorId }));
    }
}
