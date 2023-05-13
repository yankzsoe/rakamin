using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiExample.Model;

namespace WebApiExample.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase {
        private readonly DataContext.AppDataContext _context;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(DataContext.AppDataContext context, ILogger<DepartmentController> logger) {
            _context = context;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetDepartments() {
            var data = await _context.Departments.ToListAsync();
            if (data == null || data.Count == 0) {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDepartments(int id) {
            var data = await _context.Departments.FirstOrDefaultAsync(c => c.Id == id);
            if (data == null) {
                return NotFound();
            }
            return Ok(data);
        }



        [HttpPost("")]
        public async Task<IActionResult> PostDepartments(Department Department) {
            await _context.Departments.AddAsync(Department);
            var result = await _context.SaveChangesAsync();
            if (result > 0) {
                return StatusCode(201, Department);
            }
            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutDepartments(int id, Department Department) {
            var data = await _context.Departments.FirstOrDefaultAsync(c => c.Id == id);
            if (data == null) {
                return NotFound();
            }
            data.Id = id;
            data.DepartmentName = Department.DepartmentName;
            data.Description = Department.Description;
            data.IsActive = Department.IsActive;
            var result = await _context.SaveChangesAsync();
            if (result > 0) {
                Department.Id = id;
                return StatusCode(201, Department);
            }
            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDepartments(int id) {
            var cust = await _context.Departments.FirstOrDefaultAsync(c => c.Id == id);
            if (cust == null) {
                return NotFound();
            }
            _context.Departments.Remove(cust);
            var result = await _context.SaveChangesAsync();
            if (result > 0) {
                return Ok("Successfully");
            }
            return BadRequest();
        }

    }
}
