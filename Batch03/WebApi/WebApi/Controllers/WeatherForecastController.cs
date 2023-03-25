using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebApi.Controllers {
    [ApiController]
    [Route("api/cuaca")]
    public class WeatherForecastController : ControllerBase {
        private static readonly string[] Summaries = new[] {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private IMemoryCache _cache;
        private const string cacheName = "WeatherForecastList";


        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMemoryCache cache) {
            _logger = logger;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get(int id) {
            //_logger.Log(LogLevel.Error, "Test Error");
            _logger.Log(LogLevel.Information, "Begin get data.");
            if (_cache.TryGetValue(cacheName, out IEnumerable<WeatherForecast> weatherForecasts)) {
                _logger.Log(LogLevel.Information, "Get data from cache");
            } else {
                _logger.Log(LogLevel.Information, "Get data from dummy");
                try {
                    weatherForecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast {
                        Date = DateTime.Now.AddDays(index),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                    }).ToArray();

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(3))
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                        .SetPriority(CacheItemPriority.Normal)
                        .SetSize(0);
                    _cache.Set(cacheName, weatherForecasts, cacheEntryOptions);
                } catch (ArgumentOutOfRangeException ex) {
                    return StatusCode(500, $"Internal Server Error - {ex.Message}");
                } catch (ArithmeticException ex) {
                    return StatusCode(500, $"Internal Server Error - {ex.Message}");
                }
            }

            return Ok(weatherForecasts);
        }
    }
}