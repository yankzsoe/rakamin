﻿using Microsoft.AspNetCore.Mvc;
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

        /*
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
        */
    }
}
