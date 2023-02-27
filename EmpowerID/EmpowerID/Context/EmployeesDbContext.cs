using EmpowerID.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmpowerID.Context
{
    public class EmployeesDbContext : DbContext
    {
        protected override void OnConfiguring
       (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "EmployeesDb");
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
