using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orbita.BackEnd.Api.Models;

namespace Orbita.BackEnd.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly OrbitaContext _context;

        public StudentsController(OrbitaContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            return await _context.Students.ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("{ra}")]
        public async Task<ActionResult<Student>> GetStudent(string ra)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            var student = await _context.Students.FirstAsync(x => x.RA == ra);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{RA}")]
        public async Task<IActionResult> PutStudent(string RA, Student student)
        {
            if (RA != student.RA)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(RA))
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

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'OrbitaContext.Students'  is null.");
            }
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { ra = student.RA }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{ra}")]
        public async Task<IActionResult> DeleteStudent(string ra)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            var student = await _context.Students.FirstAsync(x => x.RA == ra);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(string RA)
        {
            return (_context.Students?.Any(e => e.RA == RA)).GetValueOrDefault();
        }
    }
}
