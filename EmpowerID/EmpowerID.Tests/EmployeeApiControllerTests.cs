using EmpowerID.Api;
using EmpowerID.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EmpowerID.Tests
{
    public class EmployeeApiControllerTests
    {
        private readonly Mock<IEmployeesRepository> _mockRepo;
        private readonly EmployeesController _controller;

        public EmployeeApiControllerTests()
        {
            _mockRepo = new Mock<IEmployeesRepository>();
            _controller = new EmployeesController(_mockRepo.Object);
        }

        [Fact]
        public void Index_ActionExecutes_ReturnsViewForIndex()
        {
            var result = _controller.GetAllEmployees(string.Empty);
            Assert.IsType<IActionResult>(result);
        }
    }
}
