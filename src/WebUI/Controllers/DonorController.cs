using Microsoft.AspNetCore.Mvc;
using TransfusionAPI.Application.Common.Security;
using TransfusionAPI.Application.Donors.Queries.GetDonorPersonalInfoQuery;
using TransfusionAPI.Domain.Constants;
using TransfusionAPI.WebUI.Filters;

namespace TransfusionAPI.WebUI.Controllers;

[Authorize(Roles = SupportedRoles.Donor)]
public class DonorController : ApiControllerBase
{

    [HttpGet("{donorId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DonorPersonalInfoDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDonor([FromRoute(Name = "donorId")] int donorId)
    {
        var query = new GetDonorPersonalInfoQuery() { DonorId = donorId };
        var getDonorPersonalInfoQueryResult = await Mediator.Send(query);

        if (!getDonorPersonalInfoQueryResult.Succeeded)
            return ApiExceptionFilterAttribute.GenerateBadRequestProblemDetails(getDonorPersonalInfoQueryResult);

        return Ok(getDonorPersonalInfoQueryResult.Payload);
    }
}
