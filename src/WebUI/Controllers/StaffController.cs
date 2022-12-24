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

    //[HttpPost]
    //[ProducesResponseType(StatusCodes.Status201Created, Type = typeof())]
    //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    //public async Task<IActionResult> RegisterDonor(CreateAdministratorCommand createAdministratorCommand)
    //{
    //    var result = await Mediator.Send(createAdministratorCommand);
    //    if (!result.Succeeded)
    //        return ApiExceptionFilterAttribute.GenerateBadRequestProblemDetails();

    //    return CreatedAtAction(nameof(DonorController.GetDonor), "Donor", new { donorId = result.Payload.Id }, result.Payload);
    //}
}
