using System.Text;
using System.Text.Json;
using WebApiExample.Dtos.Employee;
using WebApiExample.Dtos.Employee.Common;
using WebApiExample.Model;

namespace WebApiExample.Services
{
    public class DummyRestApiServices {
        private readonly ILogger<DummyRestApiServices> _logger;
        private readonly HttpClient _httpClient;

        public DummyRestApiServices(ILogger<DummyRestApiServices> logger, HttpClient httpClient) {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<EmployeeListResponse> GetEmployeesAsync() {
            var response = await _httpClient.GetAsync("employees");
            response.EnsureSuccessStatusCode();
            using (var responseStream = await response.Content.ReadAsStreamAsync()) {
                return await JsonSerializer.DeserializeAsync<EmployeeListResponse>(responseStream);
            }
        }

        public EmployeeResponse GetEmployeeById(int id) {
            var response = _httpClient.GetAsync($"employee/{id}").Result;
            try {
                response.EnsureSuccessStatusCode();
                using (var responseStream = response.Content.ReadAsStreamAsync().Result) {
                    return JsonSerializer.Deserialize<EmployeeResponse>(responseStream);
                }
            } catch (Exception) {
                throw;
            }
        }


        public async Task<EmployeeResponse> PostEmployeeAsync(EmployeeDto employee) {
            var data = JsonSerializer.Serialize(employee);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("create", content);
            response.EnsureSuccessStatusCode();
            using (var responseStream = await response.Content.ReadAsStreamAsync()) {
                return await JsonSerializer.DeserializeAsync<EmployeeResponse>(responseStream);
            }
        }

        public async Task<EmployeeResponse> PutEmployeeAsync(EmployeeDto employee) {
            var data = JsonSerializer.Serialize(employee);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"update/{employee.id}", content);
            response.EnsureSuccessStatusCode();
            using (var responseStream = await response.Content.ReadAsStreamAsync()) {
                return await JsonSerializer.DeserializeAsync<EmployeeResponse>(responseStream);
            }
        }
    }
}
