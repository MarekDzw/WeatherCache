using WeatherCache.Repositories;

namespace WeatherCache.Services;

public class WeatherService
{
    public readonly WeatherRepository _repository;

    public WeatherService(WeatherRepository repository)
    {
        _repository = repository;
    }

    public async Task<object> GetCityWeatherForecastAsync(string city)
    {
        return await _repository.GetCityWeatherForecastAsync(city);
    }
}