using Microsoft.AspNetCore.Mvc;
using WeatherCache.Services;

namespace WeatherCache.Controllers;

[ApiController]
[Route("api/weather")]
public class WeatherController : ControllerBase
{
    private readonly WeatherService _service;

    public WeatherController(WeatherService service)
    {
        _service = service;
    }

    [HttpGet("{city}")]
    public async Task<IActionResult> GetCityWeatherForecastAsync(string city)
    {
        var result = await _service.GetCityWeatherForecastAsync(city);

        return result != null ? Ok(result) : NotFound("City not found");
    }
}