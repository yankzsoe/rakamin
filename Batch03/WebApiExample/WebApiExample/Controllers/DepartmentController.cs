using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiExample.DataContext;
using WebApiExample.Dtos.Department;
using WebApiExample.Model;

namespace WebApiExample.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase {
        private readonly AppDataContext _context;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(AppDataContext context, ILogger<DepartmentController> logger) {
            _context = context;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetDepartments() {
            var data = await _context.Departments.ToListAsync();
            if (data == null || data.Count == 0) {
                return NotFound();
            }
            var list = new List<DepartmentResponseDto>();
            foreach (var department in data) {
                list.Add(new DepartmentResponseDto {
                    Id = department.Id,
                    DepartmentName = department.DepartmentName,
                    Description = department.Description,
                    IsActive = department.IsActive,
                });
            }
            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDepartments(int id) {
            var data = await _context.Departments.FirstOrDefaultAsync(c => c.Id == id);
            if (data == null) {
                return NotFound();
            }
            var resp = new DepartmentResponseDto() {
                Id = data.Id,
                DepartmentName = data.DepartmentName,
                Description = data.Description,
                IsActive = data.IsActive,
            };
            return Ok(resp);
        }



        [HttpPost("")]
        public async Task<IActionResult> PostDepartments(DepartmentInsertDto dto) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var dept = new Department() {
                DepartmentName = dto.DepartmentName,
                Description = dto.Description,
                IsActive = dto.IsActive,
            };

            await _context.Departments.AddAsync(dept);
            var result = await _context.SaveChangesAsync();

            if (result > 0) {
                var resp = new DepartmentResponseDto() {
                    Id = dept.Id,
                    DepartmentName = dept.DepartmentName,
                    Description = dept.Description,
                    IsActive = dept.IsActive,
                };
                return StatusCode(201, resp);
            }

            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutDepartments(int id, DepartmentInsertDto dto) {
            var data = await _context.Departments.FirstOrDefaultAsync(c => c.Id == id);
            if (data == null) {
                return NotFound();
            }

            data.Id = id;
            data.DepartmentName = dto.DepartmentName;
            data.Description = dto.Description;
            data.IsActive = dto.IsActive;
            var result = await _context.SaveChangesAsync();
            
            if (result > 0) {
                return StatusCode(200, dto);
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
