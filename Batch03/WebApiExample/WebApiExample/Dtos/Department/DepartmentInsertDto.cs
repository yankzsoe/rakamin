using System.ComponentModel.DataAnnotations;

namespace WebApiExample.Dtos.Department {
    public class DepartmentInsertDto {
        [Required]
        [StringLength(100)]
        public string DepartmentName { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}
