using LibraryERD.Domain;
using LibraryERD.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryERD.Controllers
{
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet("GetAllEmployees")]
        public ActionResult<Employees> GetEmployees()
        {
            var result = _context.Employee.ToList();
            return Ok(result);
        }

        [HttpGet("GetEmployeeById/{id}")]
        public async Task<ActionResult<Employees>> GetEmployeeById(int id)
        {
            var employee = await _context.Employee.Where(e => e.EmployeeId == id).FirstOrDefaultAsync();
            if (employee is null)
            {
                return NotFound("USER NOT FOUND");
            }
            return Ok(employee);
        }

        [HttpPost("AddEmployee")]
        public async Task<ActionResult<Employees>> AddEmployee(Employees employee)
        {
            await _context.Employee.AddAsync(employee);
            await _context.SaveChangesAsync();

            return Ok(employee);
        }


        [HttpPut("UpdateEmployee")]
        public async Task<ActionResult<Employees>> UpdateEmployee(Employees updatedEmployee)
        {
            var employee = await _context.Employee.Where(e => e.EmployeeId == updatedEmployee.EmployeeId).FirstOrDefaultAsync();
            if (employee is null)
            {
                return NotFound("USER NOT FOUND");
            }
            employee.FirstName = updatedEmployee.FirstName;
            employee.LastName = updatedEmployee.LastName;
            employee.ManagerId = updatedEmployee.ManagerId;

            await _context.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpDelete("DeleteEmployee/{id}")]
        public async Task<ActionResult<Employees>> DeleteEmployee(int id)
        {
            var employee = await _context.Employee.Where(e => e.EmployeeId == id).FirstOrDefaultAsync();
            if (employee is null)
            {
                return NotFound("USER NOT FOUND");
            }
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return Ok(employee);
        }
    }
}
