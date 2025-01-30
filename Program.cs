using StackExchange.Redis;
using WeatherCache.Repositories;
using WeatherCache.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost";
    options.InstanceName = "WeatherCache";
    options.ConfigurationOptions = new ConfigurationOptions
    {
        AbortOnConnectFail = true,
        EndPoints = { options.Configuration }
    };
});
builder.Services.AddScoped<WeatherService>();
builder.Services.AddHttpClient<WeatherRepository>();
builder.Services.AddHttpClient();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();