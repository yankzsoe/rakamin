using WebAppIntermediate2.Models;

namespace WebAppIntermediate2.Dtos {
    public class EmployeeListResponse : BaseResponse {
        public List<Employee> data { get; set; }
    }
}
