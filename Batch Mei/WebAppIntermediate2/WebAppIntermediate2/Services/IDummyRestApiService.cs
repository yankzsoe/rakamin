using Microsoft.AspNetCore.Mvc;
using WebAppIntermediate2.Dtos;

namespace WebAppIntermediate2.Services {
    public interface IDummyRestApiService {
        Task<EmployeeListResponse> GetEmployeesAsync();
        EmployeeResponse GetEmployeeById(int id);
    }
}
