using Microsoft.AspNetCore.Mvc;
using WebApplication1.helper;

namespace WebApplication1.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class IntermediateSatuController : ControllerBase {
        public Intermediate1Sample sample1;

        /// <summary>
        /// Contoh untuk proses tanpa async
        /// </summary>
        /// <param name="detik"></param>
        /// <returns></returns>
        [HttpGet("SampleNotAsync")]
        public IActionResult ContohBukanAsync(int detik) {

            var startTime = DateTime.Now;
            sample1 = new Intermediate1Sample();
            sample1.SampleProses("proses 1", detik);
            var endTime = DateTime.Now;
            var duration = endTime.Subtract(startTime);
            return Ok($"durasi proses: {duration}");

        }

        /// <summary>
        /// Contoh untuk proses dengan async
        /// </summary>
        /// <param name="detik"></param>
        /// <returns></returns>
        [HttpGet("SampleAsync")]
        public IActionResult ContohAsync(int detik) {
            var startTime = DateTime.Now;
            sample1 = new Intermediate1Sample();
            _ = sample1.SampleProsesAsync("proses 1", detik);
            _ = sample1.SampleProsesAsync("proses 2", detik);
            var endTime = DateTime.Now;
            var duration = endTime.Subtract(startTime);
            return Ok($"durasi proses: {duration}");
        }

        /// <summary>
        /// Contoh untuk proses dengan async tapi prosesnya ditunggu satu persatu
        /// </summary>
        /// <param name="detik"></param>
        /// <returns></returns>
        [HttpGet("SampleMixedAsync")]
        public async Task<IActionResult> SampleMixedAsync(int detik) {
            var startTime = DateTime.Now;
            sample1 = new Intermediate1Sample();
            await sample1.SampleProsesAsync("proses 1", detik);
            await sample1.SampleProsesAsync("proses 2", detik);
            var endTime = DateTime.Now;
            var duration = endTime.Subtract(startTime);
            return Ok($"durasi proses: {duration}");
        }

        /// <summary>
        /// Contoh untuk proses dengan async tapi prosesnya tetap berjalan berbarengan
        /// </summary>
        /// <param name="detik"></param>
        /// <returns></returns>
        [HttpGet("SampleMixedCustomAsync")]
        public IActionResult SampleMixedCustom(int detik) {
            var startTime = DateTime.Now;

            sample1 = new Intermediate1Sample();
            Task task1 = Task.Run(() => sample1.SampleProsesAsync("proses 1", detik));
            Task task2 = Task.Run(() => sample1.SampleProsesAsync("proses 1", detik));
            Task.WaitAll(task1, task2);

            var endTime = DateTime.Now;
            var duration = endTime.Subtract(startTime);
            return Ok($"durasi proses: {duration}");
        }

        /// <summary>
        /// Contoh untuk proses macam-macam looping
        /// </summary>
        /// <param name="jumlah"></param>
        /// <returns></returns>
        [HttpGet("SampleLooping")]
        public IActionResult SampleLooping(int jumlah) {
            var startTime = DateTime.Now;

            sample1 = new Intermediate1Sample();
            Task task1 = Task.Run(() => sample1.SampleLooping(jumlah));

            return Ok("Succesfully");
        }

        /// <summary>
        /// Contoh untuk proses macam-macam looping dengan break
        /// </summary>
        /// <param name="jumlah"></param>
        /// <returns></returns>
        [HttpGet("SampleBreakLooping")]
        public IActionResult SampleBreakLooping(int jumlah) {
            var startTime = DateTime.Now;

            sample1 = new Intermediate1Sample();
            sample1.SampleLoopingDenganBreak(jumlah);

            return Ok("Succesfully");
        }

        PersegiPanjang persegi;
        [HttpGet("hitungpersegi")]
        public IActionResult HitungLuas(int panjang, int lebar) {
            persegi = new PersegiPanjang(panjang, lebar);
            var result = persegi.HitungLuas();
            return Ok(result);
        }
    }
}
