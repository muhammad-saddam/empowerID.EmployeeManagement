using EmpowerID.Entities;

namespace EmpowerID.Repository
{
    public interface IEmployeesRepository
    {
        public Task<List<Employee>> GetAllEmployees(string? searchText);
        public Task<Employee> SaveEmployee(Employee employee);
        public Task<Employee> GetEmployeeById(int employeeId);
        public Task<bool> DeleteEmployeeById(int employeeId);
    }
}
