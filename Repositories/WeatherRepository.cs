using System.Text.Json;

namespace WeatherCache.Repositories;

public class WeatherRepository
{
    private readonly string _apiKey;
    private readonly string _baseUrl;
    private readonly HttpClient _client;

    public WeatherRepository(HttpClient client, IConfiguration configuration)
    {
        _client = client;
        _apiKey = configuration["OpenWeather:ApiKey"];
        _baseUrl = configuration["OpenWeather:BaseUrl"];
    }

    public async Task<object> GetCityWeatherForecastAsync(string city)
    {
        var url = $"{_baseUrl}weather?q={city}&appid={_apiKey}&units=metric";
        var response = await _client.GetAsync(url);

        if (!response.IsSuccessStatusCode) return null;

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<object>(json);
    }
}