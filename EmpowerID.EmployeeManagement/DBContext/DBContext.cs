using EmpowerID.EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EmpowerID.EmployeeManagement.DBContext
{
    public class EmployeesDbContext : DbContext
    {
        protected override void OnConfiguring
       (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "EmployeesDb");
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmpowerID.Models.EmployeesViewModel> EmployeesViewModel { get; set; }
    }
}
