using System.Text.Json;
using WebAppIntermediate2.Dtos;

namespace WebAppIntermediate2.Services {
    public class DummyRestApiService : IDummyRestApiService {
        private readonly HttpClient _httpClient;
        public DummyRestApiService(HttpClient httpClient) {
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
    }
}
