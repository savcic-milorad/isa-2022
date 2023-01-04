using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransfusionAPI.Application.Identity.Commands.CreateDonor;
using TransfusionAPI.Application.Identity.Commands.Login;
using TransfusionAPI.Application.Identity.Queries.GetApplicationUser;
using TransfusionAPI.WebUI.Filters;

namespace TransfusionAPI.WebUI.Controllers;

[AllowAnonymous]
public class IdentityController : ApiControllerBase
{
    [HttpGet("ApplicationUsers/{applicationUserId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApplicationUserDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetApplicationUser([FromRoute] string applicationUserId)
    {
        var query = new GetApplicationUserQuery() { ApplicationUserId = applicationUserId };

        var result = await Mediator.Send(query);

        if (!result.Succeeded)
            return ApiExceptionFilterAttribute.GenerateBadRequestProblemDetails(result);

        return Ok(result.Payload);
    }

    [HttpPost("Donors")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreatedDonorDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> RegisterDonor(CreateDonorCommand createDonorCommand)
    {
        var result = await Mediator.Send(createDonorCommand);
        if (!result.Succeeded)
            return ApiExceptionFilterAttribute.GenerateBadRequestProblemDetails();

        return CreatedAtAction(nameof(DonorController.GetDonor), "Donor", new { donorId = result.Payload.Id }, result.Payload);
    }

    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginSuccessDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Login(LoginCommand loginCommand)
    {
        var result = await Mediator.Send(loginCommand);
        if (!result.Succeeded)
            return ApiExceptionFilterAttribute.GenerateForbiddenAccessProblemDetails();

        return Ok(result.Payload);
    }
}
