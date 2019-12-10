using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeRound1.Models;

namespace PracticeRound1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly Contosouniversitypracticeround1Context _context;

        public DepartmentsController(Contosouniversitypracticeround1Context context)
        {
            _context = context;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartment()
        {
            return await _context.Department.Where(x => x.IsDeleted != true).ToListAsync();
        }

        // GET: api/Departments/Course/Count
        [HttpGet("Course/Count")]
        public async Task<ActionResult<IEnumerable<DepartmentCourseCountVM>>> GetDepartmentCount()
        {
            return await _context.DepartmentCourseCountVM.FromSqlRaw("SELECT * FROM dbo.VwDepartmentCourseCount").ToListAsync();
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _context.Department.Where(x => x.IsDeleted != true).FirstOrDefaultAsync(x => x.DepartmentId == id);

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, Department dept)
        {
            if (id != dept.DepartmentId)
            {
                return BadRequest();
            }

            await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC dbo.Department_Update {dept.DepartmentId}, {dept.Name}, {dept.Budget}, {dept.StartDate}, {dept.InstructorId}, {dept.RowVersion}");

            return NoContent();
        }

        // POST: api/Departments
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(Department dept)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC dbo.Department_Insert {dept.Name}, {dept.Budget}, {dept.StartDate}, {dept.InstructorId}");

            return CreatedAtAction("GetDepartment", new { id = dept.DepartmentId }, dept);
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Department>> DeleteDepartment(int id)
        {
            var department = await _context.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            department.IsDeleted = true;
            await _context.SaveChangesAsync();

            return department;
        }

        // DELETE: api/Departments/5
        [HttpDelete("SP/{id}")]
        public async Task<ActionResult<Department>> DeleteDepartmentBySP(int id)
        {
            var dept = await _context.Department.FindAsync(id);
            if (dept == null)
            {
                return NotFound();
            }

            await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC dbo.Department_Delete {dept.DepartmentId}, {dept.RowVersion}");

            return dept;
        }

        private bool DepartmentExists(int id)
        {
            return _context.Department.Any(e => e.DepartmentId == id);
        }
    }
}
