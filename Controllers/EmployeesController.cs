using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public EmployeesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //Http Verbs
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var allEmployees = dbContext.Employees.ToList();

            return Ok(allEmployees);
        }
        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        {

            var employeeEntity = new Employee()
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Salary = addEmployeeDto.Salary,
                Phone = addEmployeeDto.Phone
            };


            dbContext.Employees.Add(employeeEntity);
            dbContext.SaveChanges();

            return Ok(employeeEntity);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployeeById(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = dbContext.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }
            employee.Email = updateEmployeeDto.Email;
            employee.Name = updateEmployeeDto.Name;
            employee.Salary = updateEmployeeDto.Salary;
            employee.Phone = updateEmployeeDto.Phone;

            dbContext.SaveChanges();
            return Ok(employee);

        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public IActionResult DeleteEmployeeById(Guid id)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();

            return Ok("Deleted");
        }
    }
}
