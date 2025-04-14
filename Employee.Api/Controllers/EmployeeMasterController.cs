using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Employee.Api.Model;

namespace Employee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeMasterController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        public EmployeeMasterController(EmployeeDbContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeMaster
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeData>>> GetEmployees()
        {
            //return await _context.Employees.OrderBy(e => e.employeeId).ToListAsync();
            return await _context.Employees
                        .Where(e => e.isDeleted == 0 || e.isDeleted == null) // soft delete filter
                        .OrderBy(e => e.employeeId)
                        .ToListAsync();
        }

        // GET: api/EmployeeMaster/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeData>> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/EmployeeMaster/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeData employee)
        {
            employee.employeeId = id;
            employee.createdDate = DateTime.Now;
            //if (id != employee.employeeId)
            //{
            //    return BadRequest();
            //}

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Employee updated successfully.", data = employee });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EmployeeMaster
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeData>> PostEmployee(EmployeeData employee)
        {
            employee.employeeId = 0;
            employee.createdDate = DateTime.Now;
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.employeeId }, employee);
        }

        // DELETE: api/EmployeeMaster/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            employee.isDeleted = 1;
            //_context.Employees.Remove(employee);
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.employeeId == id);
        }
    }
}
