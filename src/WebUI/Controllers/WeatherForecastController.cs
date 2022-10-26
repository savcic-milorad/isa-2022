using TransfusionAPI.Application.WeatherForecasts.Queries.GetWeatherForecasts;
using Microsoft.AspNetCore.Mvc;

namespace TransfusionAPI.WebUI.Controllers;

//[Authorize]
public class WeatherForecastController : ApiControllerBase
{
    /// <summary>
    /// Gets weather
    /// </summary>
    /// <param name="item"></param>
    /// <returns>Weather</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /
    ///
    /// </remarks>
    /// <response code="201">Returns the newly created item</response>
    /// <response code="400">If the item is null</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        return await Mediator.Send(new GetWeatherForecastsQuery());
    }
}
