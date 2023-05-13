using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiExample.Model {

    public class Pegawai {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Nama { get; set; }

        public DateTime Dob { get; set; }

        [StringLength(250)]
        public string Alamat { get; set; }

        public Department Department { get; set; }
        public int DepartmentId { get; set; }
    }
}
