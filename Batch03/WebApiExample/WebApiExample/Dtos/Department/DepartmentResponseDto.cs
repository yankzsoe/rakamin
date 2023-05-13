using System.ComponentModel.DataAnnotations;

namespace WebApiExample.Dtos.Department {
    public class DepartmentResponseDto {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
