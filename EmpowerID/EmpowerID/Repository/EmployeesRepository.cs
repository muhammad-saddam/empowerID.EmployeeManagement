using EmpowerID.Context;
using EmpowerID.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmpowerID.Repository
{
    public class EmployeesRepository : IEmployeesRepository
    {
        public async Task<List<Employee>> GetAllEmployees(string? searchText)
        {
            using (var context = new EmployeesDbContext())
            {
                var query = context.Employees.AsQueryable();

                if (!string.IsNullOrEmpty(searchText))
                    query = query.Where(x => x.Name.Contains(searchText,StringComparison.OrdinalIgnoreCase));
                return await query.ToListAsync();
            }
        }

        public async Task<Employee> SaveEmployee(Employee employee)
        {
            using (var context = new EmployeesDbContext())
            {
                if(employee.Id == 0)
                await context.Employees.AddAsync(employee);
                else
                    context.Employees.Update(employee);
                int employeeId =  await context.SaveChangesAsync();
                employee.Id = employeeId;
                return employee;
            }
        }

        public async Task<Employee> GetEmployeeById(int employeeId)
        {
            using (var context = new EmployeesDbContext())
            {
                var result = await context.Employees.SingleAsync(x => x.Id == employeeId);
                return result;
            }
        }

        public async Task<bool> DeleteEmployeeById(int employeeId)
        {
            using (var context = new EmployeesDbContext())
            {
                context.Employees.Remove(await GetEmployeeById(employeeId));
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
