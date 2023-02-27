using EmpowerID.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmpowerID.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IConfiguration _configuration;
        public EmployeesController(IConfiguration configuration)
        {
            _configuration= configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchText)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(string.Format("{0}/api/Employees/", _configuration.GetValue<string>("AppBaseUrl")));
                var result = await client.PostAsJsonAsync("GetAllEmployees",searchText ?? string.Empty);
                if (result.IsSuccessStatusCode)
                {
                    var allEmployees = await result.Content.ReadFromJsonAsync<List<EmployeesViewModel>>();
                    return View(allEmployees);
                }     
                else
                    return View(new List<EmployeesViewModel>());
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new EmployeesViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employeeData = await GetEmployeeDataById(id);
            return View(employeeData);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var employeeData = await GetEmployeeDataById(id);
            return View(employeeData);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var employeeData = await GetEmployeeDataById(id);
            return View(employeeData);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEmployeeData(EmployeesViewModel employeesViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(string.Format("{0}/api/Employees/", _configuration.GetValue<string>("AppBaseUrl")));
                var result = await client.PostAsJsonAsync("SaveEmployee", employeesViewModel);
                if (result.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError(string.Empty,"Some error occurred while creating employee");
                    return View("Create", employeesViewModel);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(string.Format("{0}/api/Employees/", _configuration.GetValue<string>("AppBaseUrl")));
                var result = await client.PostAsJsonAsync("DeleteEmployeeById", id);
                if (result.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError(string.Empty, "Some error occurred while deleting employee");
                    return View("Delete", new EmployeesViewModel());
                }
            }

        }

        private async Task<EmployeesViewModel> GetEmployeeDataById(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(string.Format("{0}/api/Employees/", _configuration.GetValue<string>("AppBaseUrl")));
                var result = await client.PostAsJsonAsync("GetEmployeeById", id);
                if(result.IsSuccessStatusCode)
                {
                    var employee = await result.Content.ReadFromJsonAsync<EmployeesViewModel>();
                    return employee ?? null;
                }
               else
                    return null;
            }
        }
    }
}
