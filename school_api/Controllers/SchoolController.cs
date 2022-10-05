using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using school_api.Data;
using school_api.Model;

namespace school_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SchoolController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/School
        [HttpGet]
        public async Task<ActionResult<IEnumerable<School>>> GetSchool()
        {
            return await _context.School.Include("Address").ToListAsync();
        }

        // GET: api/School/5
        [HttpGet("{id}")]
        public async Task<ActionResult<School>> GetSchool(int id)
        {
            var school = await _context.School.Where(x => x.Id == x.Id)
                .Include("Address")
                .FirstOrDefaultAsync(); ;

            if (school == null)
            {
                return NotFound();
            }

            return school;
        }
        // POST: api/School
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<School>> PostSchool(School school)
        {
            //var s = await _context.School.FindAsync(1);
            //school.Id = 1;
            if (SchoolExists(1))
            {
                _context.School.Update(school);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.School.Add(school);
                await _context.SaveChangesAsync();
            }
            

            return CreatedAtAction("GetSchool", new { id = school.Id }, school);
        }

        private bool SchoolExists(int id)
        {
            return _context.School.Any(e => e.Id == id);
        }
    }
}
