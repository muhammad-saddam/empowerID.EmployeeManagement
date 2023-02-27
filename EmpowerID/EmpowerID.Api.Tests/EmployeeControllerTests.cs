using EmpowerID.Entities;
using EmpowerID.Models;
using EmpowerID.Repository;
using Moq;

namespace EmpowerID.Api.Tests
{
    public class EmployeeControllerTests
    {
        private readonly EmployeesController _employeesController;
        private readonly Mock<IEmployeesRepository> _employeesRepository = new Mock<IEmployeesRepository>();

        public EmployeeControllerTests()
        {
            _employeesController = new EmployeesController(_employeesRepository.Object);
        }

        [Fact]
        public async Task GetAllEmployees_ShouldReturn_EmployeesList()
        {
            // Arrange
            var searchString = "test";
            // Act
            var employees = await _employeesController.GetAllEmployees(searchString);
            // Assert
            Assert.IsType<List<EmployeesViewModel>>(employees);
        }

        [Fact]
        public async Task Get_Employee_ShouldReturnEmployee_WhenEmployeeExist()
        {
            // Arrange
            var employeeId = int.MaxValue;
            var employeeName = "Saddam";
            var empDepartment = "IT";
            var employee = new Employee
            {
                Id = employeeId,
                Name = employeeName,
                Department = empDepartment
            };
            _employeesRepository.Setup(x => x.GetEmployeeById(employeeId)).ReturnsAsync(employee);
            // Act
            var emp = await _employeesController.GetEmployeeById(employeeId);
            // Assert
            Assert.Equal(employeeId,emp.Id);
            Assert.Equal(employeeName, emp.Name);
            Assert.Equal(empDepartment, emp.Department);
        }

        [Fact]
        public async Task Get_Employee_ShouldReturnNothing_WhenEmployeeDoesNotExist()
        {
            // Arrnge         
            _employeesRepository.Setup(x => x.GetEmployeeById(It.IsAny<int>())).ReturnsAsync(() => null);
            // Act          
            var employee = await _employeesController.GetEmployeeById(int.MaxValue);
            // Assert                  
            Assert.Null(employee);
        }
    }
}