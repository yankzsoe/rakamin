using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiExample.Model {
    public class Department {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string DepartmentName { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}
