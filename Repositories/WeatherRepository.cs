using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using WeatherCache.Extensions;

namespace WeatherCache.Repositories;

public class WeatherRepository
{
    private readonly string _apiKey;
    private readonly string _baseUrl;
    private readonly IDistributedCache _cache;
    private readonly HttpClient _client;

    public WeatherRepository(HttpClient client, IConfiguration configuration, IDistributedCache cache)
    {
        _client = client;
        _apiKey = configuration["OpenWeather:ApiKey"];
        _baseUrl = configuration["OpenWeather:BaseUrl"];
        _cache = cache;
    }

    public async Task<object> GetCityWeatherForecastAsync(string city)
    {
        var cacheKey = $"city:{city}";

        var json = await _cache.GetOrSetAsync(cacheKey,
            async () =>
            {
                var url = $"{_baseUrl}weather?q={city}&appid={_apiKey}&units=metric";
                var response = await _client.GetAsync(url);

                if (!response.IsSuccessStatusCode) return null;

                return await response.Content.ReadAsStringAsync();
            })!;

        if (json == null) return null;


        return JsonSerializer.Deserialize<object>(json);
    }
}