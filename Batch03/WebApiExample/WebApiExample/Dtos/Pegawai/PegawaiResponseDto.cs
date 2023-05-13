using System.ComponentModel.DataAnnotations;

namespace WebApiExample.Dtos.Pegawai {
    public class PegawaiResponseDto {
        public int Id { get; set; }
        public string Nama { get; set; }
        public DateTime Dob { get; set; }
        [StringLength(250)]
        public string Alamat { get; set; }
        public int DepartmentId { get; set; }
    }
}
