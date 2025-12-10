using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Controllers
{
    //localhost:xxxx/api/
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly ApplicationDbContext _dbContext;

        public EmployeeController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            var allEmployees = _dbContext.Employees.ToList();
            /*var employees = new[]
            {
                new { Id = 1, Name = "Alice Johnson", Position = "Software Engineer" },
                new { Id = 2, Name = "Bob Smith", Position = "Project Manager" },
                new { Id = 3, Name = "Charlie Brown", Position = "UX Designer" }
            };*/
            return Ok(allEmployees);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetAllEmployeeById(Guid id)
        {
            var employee = _dbContext.Employees.Find(id);

            if (employee is null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            var employeeEntity = new Models.Entities.Employee()
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary
            };

            _dbContext.Employees.Add(employeeEntity);
            _dbContext.SaveChanges();

            return Ok(employeeEntity);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = _dbContext.Employees.Find(id);

            if (employee is null)
            {
                return NotFound();
            }

            employee.Name = updateEmployeeDto.Name;
            employee.Email = updateEmployeeDto.Email;
            employee.Phone = updateEmployeeDto.Phone;
            employee.Salary = updateEmployeeDto.Salary;

            _dbContext.SaveChanges();

            return Ok(employee);
        }

        [HttpDelete]
	    [Route("{id:guid}")]
		public IActionResult DeleteEmployee(Guid id)
		{
			var employee = _dbContext.Employees.Find(id);

			if (employee is null)
			{
				return NotFound();
			}

            _dbContext.Employees.Remove(employee);
			_dbContext.SaveChanges();

			return Ok();
		}
	}
}
