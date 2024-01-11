using RareCrewTask.Models;
using System.Net.Http;
using System.Text.Json;

namespace RareCrewTask.Services
{
    public class EmployeeService
    {
        private readonly HttpClient _httpClient;

        public EmployeeService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Employee>> GetEmployeesFromApi(string apiUrl)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                var responseStream = await response.Content.ReadAsStreamAsync();
                var employees = await JsonSerializer.DeserializeAsync<List<Employee>>(responseStream);

                var aggregatedEmployees = employees.GroupBy(e => e.EmployeeName).Select(g => new Employee
                                            {
                                                EmployeeName = g.Key,
                                                TotalTimeWorked = TimeSpan.FromTicks(g.Sum(e => (e.EndTimeUtc - e.StarTimeUtc).Ticks))
                                            }).OrderByDescending(e => e.TotalTimeWorked).ToList();

                return aggregatedEmployees;
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Error fetching data from the API.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while processing the request.", ex);
            }
        } 
    }
}
