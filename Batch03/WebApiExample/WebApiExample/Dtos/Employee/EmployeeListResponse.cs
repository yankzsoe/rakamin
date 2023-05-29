using WebApiExample.Dtos.Employee.Common;
using WebApiExample.Model;

namespace WebApiExample.Dtos.Employee {
    public class EmployeeListResponse : BaseResponse {
        public List<EmployeeDto> data { get; set; }
    }
}
