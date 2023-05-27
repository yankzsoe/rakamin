using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebAppIntermediate2.Dtos;
using WebAppIntermediate2.Services;

namespace WebAppIntermediate2.Controllers {
    //[Route("api/[controller]")]
    [Route("api/karyawan")]
    [ApiController]
    public class EmployeeController : ControllerBase {
        private readonly ILogger<EmployeeController> _logger;
        private readonly DummyRestApiService _dummyRestApi;

        private IMemoryCache _cache;
        private const string cacheName = "Employees";

        public EmployeeController(ILogger<EmployeeController> logger, DummyRestApiService dummyRestApiService, IMemoryCache cache) {
            _dummyRestApi = dummyRestApiService;
            _logger = logger;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        [HttpGet("")]
        public async Task<IActionResult> GetEmployees() {
            var startTime = DateTime.Now;
            _logger.LogInformation($"Request dimulai pada {startTime}");
            var result = await _dummyRestApi.GetEmployeesAsync();
            var endTime = DateTime.Now;
            _logger.LogInformation($"Request selesai pada {endTime}, dengan durasi {endTime.Subtract(startTime)}");
            return Ok(result);
        }

        [HttpGet("ById")]
        public IActionResult GetEmployeeById(int id) {
            try {
                var result = _dummyRestApi.GetEmployeeById(id);
                return Ok(result);
            } catch (HttpRequestException ex) when (ex.StatusCode.HasValue) {
                var statusCode = (int)ex.StatusCode.Value;
                return StatusCode(statusCode, ex.Message);
            } catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("/cahce/Employees")]
        public async Task<IActionResult> GetCacheEmployees() {
            var cacheKey = cacheName;
            var result = new EmployeeListResponse();

            if (_cache.TryGetValue(cacheKey, out EmployeeListResponse response)) {
                _logger.Log(LogLevel.Information, "Get data from cache");
                return Ok(response);
            }

            try {
                _logger.Log(LogLevel.Information, "Get data from services");
                result = await _dummyRestApi.GetEmployeesAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(3))
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                .SetPriority(CacheItemPriority.Normal);

                _cache.Set(cacheKey, result, cacheEntryOptions);
                return Ok(result);
            } catch (HttpRequestException ex) when (ex.StatusCode.HasValue) {
                var statusCode = (int)ex.StatusCode.Value;
                return StatusCode(statusCode, ex.Message);
            } catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("/cahce/EmployeeById")]
        public IActionResult GetCacheEmployeeById(int id) {
            var cacheKey = cacheName + id.ToString();
            var result = new EmployeeResponse();

            if (_cache.TryGetValue(cacheKey, out EmployeeResponse response)) {
                _logger.Log(LogLevel.Information, "Get data from cache");
                return Ok(response);
            }

            try {
                _logger.Log(LogLevel.Information, "Get data from services");
                result = _dummyRestApi.GetEmployeeById(id);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(3))
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                .SetPriority(CacheItemPriority.Normal);

                _cache.Set(cacheKey, result, cacheEntryOptions);
                return Ok(result);
            } catch (HttpRequestException ex) when (ex.StatusCode.HasValue) {
                var statusCode = (int)ex.StatusCode.Value;
                return StatusCode(statusCode, ex.Message);
            } catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("/sampleno20")]
        public IActionResult GetFile() {
            var fileName = "esb-architecture.png";
            var path = Path.Combine(Environment.CurrentDirectory,"Image", fileName);

            if (!System.IO.File.Exists(path)) {
                return NotFound();
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            var base64string = Convert.ToBase64String(fileBytes);
            return Ok(base64string);

        }

        [HttpGet("/sampleno20download")]
        public IActionResult GetFileDownload() {
            var fileName = "esb-architecture.png";
            var path = Path.Combine(Environment.CurrentDirectory, "Image", fileName);

            if (!System.IO.File.Exists(path)) {
                return NotFound();
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, "application/octet-stream", fileName);

        }
    }
}
