using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiExample.DataContext;
using WebApiExample.Dtos.Pegawai;
using WebApiExample.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiExample.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PegawaiController : ControllerBase {
        private readonly AppDataContext _context;
        private readonly ILogger<PegawaiController> _logger;

        public PegawaiController(AppDataContext context, ILogger<PegawaiController> logger) {
            _context = context;
            _logger = logger;
        }

        // GET: api/<PegawaiController>
        [HttpGet]
        public async Task<IActionResult> Get() {
            var data = await _context.Pegawai.ToListAsync();
            if (data == null || data.Count == 0) {
                return NotFound();
            }

            var list = new List<PegawaiResponseDto>();
            foreach (var item in data) {
                list.Add(new PegawaiResponseDto {
                    Id = item.Id,
                    DepartmentId = item.DepartmentId,
                    Nama = item.Nama,
                    Alamat = item.Alamat,
                    Dob = item.Dob,
                });
            }

            return Ok(list);
        }

        // GET api/<PegawaiController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) {
            var data = await _context.Pegawai.FirstOrDefaultAsync(e => e.Id == id);
            if (data == null) {
                return NotFound();
            }
            var result = new PegawaiResponseDto() {
                Id = data.Id,
                DepartmentId = data.DepartmentId,
                Nama = data.Nama,
                Alamat = data.Alamat,
                Dob = data.Dob,
            };
            return Ok(data);
        }

        // POST api/<PegawaiController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PegawaiInsertDto param) {
            var pegawai = new Pegawai() {
                Nama = param.Nama,
                Dob = param.Dob,
                Alamat = param.Alamat,
                DepartmentId = param.DepartmentId,
            };

            await _context.Pegawai.AddAsync(pegawai);
            var result = await _context.SaveChangesAsync();
            if (result > 0) {
                var resp = new PegawaiResponseDto() {
                    Id = pegawai.Id,
                    Nama = pegawai.Nama,
                    Alamat = pegawai.Alamat,
                    Dob = pegawai.Dob,
                    DepartmentId = pegawai.DepartmentId
                };
                return StatusCode(201, resp);
            }

            return BadRequest();
        }

        // PUT api/<PegawaiController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PegawaiInsertDto dto) {
            var data = await _context.Pegawai.FirstOrDefaultAsync(x => x.Id == id);
            if (data == null) { return NotFound(); }

            data.Nama = dto.Nama;
            data.Dob = dto.Dob;
            data.Alamat = dto.Alamat;
            data.DepartmentId = dto.DepartmentId;

            var result = await _context.SaveChangesAsync();
            if (result > 0) {
                var resp = new PegawaiResponseDto() {
                    Id = data.Id,
                    Nama = data.Nama,
                    Alamat = data.Alamat,
                    Dob = data.Dob,
                    DepartmentId = data.DepartmentId,
                };
                return StatusCode(200, resp);
            }

            return BadRequest();
        }

        // DELETE api/<PegawaiController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var data = await _context.Pegawai.FirstOrDefaultAsync(c => c.Id == id);
            if (data == null) {
                return NotFound();
            }
            _context.Pegawai.Remove(data);
            var result = await _context.SaveChangesAsync();
            if (result > 0) {
                return Ok("Successfully");
            }
            return BadRequest();
        }
    }
}
