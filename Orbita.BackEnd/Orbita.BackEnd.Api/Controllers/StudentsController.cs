using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents(
            [FromQuery] string? search = ""
            )
        {
            if (_context.Students == null)
            {
                return NotFound();
            }

            IQueryable<Student> studentsQuery = _context.Students;

            if (!string.IsNullOrEmpty(search))
            {
                studentsQuery = studentsQuery.Where(s => s.Name.Contains(search) || s.RA.Contains(search) || s.CPF.Contains(search));
            }

            var students = await studentsQuery.ToListAsync();

            return students;
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            var student = await _context.Students.FirstAsync(x => x.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            if (StudentRAISModified(id, student.RA))
            {
                ModelState.AddModelError("ra", "O RA não pode ser editado.");
                return ValidationProblem();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

            if (StudentRAExists(student.RA))
            {
                ModelState.AddModelError("ra", "O RA informado já está cadastrado");
                return ValidationProblem();
            }
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            var student = await _context.Students.FirstAsync(x => x.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool StudentRAExists(string ra)
        {
            return (_context.Students?.Any(e => e.RA == ra)).GetValueOrDefault();
        }

        private bool StudentRAISModified(int id, string ra)
        {
            return (_context.Students?.Any(e => e.Id == id && e.RA != ra)).GetValueOrDefault();
        }
    }
}
