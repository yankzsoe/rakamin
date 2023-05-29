using WebApiExample.Dtos.Employee.Common;

namespace WebApiExample.Dtos.Employee
{
    public class EmployeeResponse : BaseResponse
    {
        public EmployeeDto data { get; set; }
    }
}
