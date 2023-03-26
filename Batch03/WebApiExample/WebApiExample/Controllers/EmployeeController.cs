using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiExample.Model;
using WebApiExample.Services;

namespace WebApiExample.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase {
        private readonly ILogger<EmployeeController> _logger;
        private readonly DummyRestApiServices _dummyRestApi;
        public EmployeeController(ILogger<EmployeeController> logger, DummyRestApiServices dummyRestApi) {
            _logger = logger;
            _dummyRestApi = dummyRestApi;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetEmployees() {
            var result = await _dummyRestApi.GetEmployeesAsync();
            return Ok(result);
        }

        [HttpGet("ById")]
        public IActionResult GetEmployeeById(int id) {
            var result = _dummyRestApi.GetEmployeeByIdAsync(id);
            return Ok(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> PostEmployee(Employee employee) {
            var result = await _dummyRestApi.PostEmployeeAsync(employee);
            return Ok(result);
        }

        [HttpPut("")]
        public async Task<IActionResult> PutEmployee(Employee employee) {
            var result = await _dummyRestApi.PutEmployeeAsync(employee);
            return Ok(result);
        }

        [HttpDelete("")]
        public async Task<IActionResult> DeleteEmployee(int id) {
            var result = await _dummyRestApi.DeleteEmployeeAsync(id);
            return Ok(result);
        }
    }
}
