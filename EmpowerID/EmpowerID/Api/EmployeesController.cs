using EmpowerID.Entities;
using EmpowerID.Models;
using EmpowerID.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmpowerID.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesRepository _employeesRepository;

        public EmployeesController(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }

        [HttpGet("{searchText}")]
        [Route("GetAllEmployees")]
        public async Task<List<EmployeesViewModel>> GetAllEmployees([FromBody]string? searchText)
        {
            try
            {
                var employees = new List<EmployeesViewModel>();

                var employeesData = await _employeesRepository.GetAllEmployees(searchText);

                if(employeesData != null && employeesData.Any())
                {
                    employees.AddRange(employeesData.Select(x => new EmployeesViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Email = x.Email,
                        DOB = x.DOB,
                        Department = x.Department
                    }).ToList());

                    return employees;

                }
                else
                    return new List<EmployeesViewModel>(); 
                
            }
            catch (Exception)
            {
                return null;
            }
           
        }

        [HttpGet("{id}")]
        [Route("GetEmployeeById")]
        public async Task<Employee> GetEmployeeById([FromBody]int id)
        {
            try
            {
                var result = await _employeesRepository.GetEmployeeById(id);

                return result;
            }
            catch (Exception)
            {
                return null;   
            }
        }

        [HttpPost]
        [Route("SaveEmployee")]
        public async Task<Employee> SaveEmployee([FromBody]EmployeesViewModel employeesViewModel)
        {
            try
            {
                var result = await _employeesRepository.SaveEmployee(new Employee
                {
                    Id = employeesViewModel.Id,
                    Name= employeesViewModel.Name,
                    Email= employeesViewModel.Email,
                    DOB = employeesViewModel.DOB,
                    Department = employeesViewModel.Department
                });

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpDelete("{id}")]
        [Route("DeleteEmployeeById")]
        public async Task<bool> DeleteEmployeeById([FromBody]int id)
        {
            try
            {
               var result = await _employeesRepository.DeleteEmployeeById(id);
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
