using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreCompatibility;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using school_api.Data;
using System.Web;
using school_api.Model;
using static System.Net.WebRequestMethods;

namespace school_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _web_hosting;
        public SchoolController(AppDbContext context, IWebHostEnvironment web_hosting)
        {
            _context = context;
            _web_hosting = web_hosting;
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
        [HttpPost("Uploads")]
        public async Task<ActionResult> UploadFile(string path)
        {
            
            
            

            var school = await _context.School.FindAsync(1);
            if(SchoolExists(1))
            {
                school.Icon = path;
                _context.Update(school);
                _context.SaveChanges();
                return Ok($"Image has been successfully uploaded");
            }
            return BadRequest("Please update the school information first, before uploading the image");
            
        }
        private bool SchoolExists(int id)
        {
            return _context.School.Any(e => e.Id == id);
        }
    }

    public class Storage
    {
        public static FirebaseStorage GetStorage()
        {
            var fo = new FirebaseStorageOptions()
            {
                ThrowOnCancel = true,
            };
            FirebaseStorage storage = new("gs://school-transport-fc879.appspot.com", fo);

            return storage;
        }
    }
    
}
