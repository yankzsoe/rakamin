using System.ComponentModel.DataAnnotations;

namespace WebApiExample.Dtos.Pegawai {
    public class PegawaiInsertDto {
        [Required]
        [StringLength(150)]
        public string Nama { get; set; }

        public DateTime Dob { get; set; }

        [StringLength(250)]
        public string Alamat { get; set; }

        public int DepartmentId { get; set; }
    }
}
