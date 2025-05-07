using System.Runtime.InteropServices;
using ASPCoreWebAPICRUD.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPCoreWebAPICRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly MyDbContext dbContext;

        public StudentAPIController(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet] //Get all student data
        public async Task<ActionResult<List<Student>>> GetStudent()
        {
            var data = await dbContext.Students.ToListAsync();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var data = await dbContext.Students.FindAsync(id);
            if (data == null) {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Student>> AddStudent(Student student)
        {
            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();
            return Ok("Student add successfully");

        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> UpdateStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }
            else
            {
                dbContext.Entry(student).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
                return Ok(student);
            }
        }
       

        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var std =await dbContext.Students.FindAsync(id);
            if (std == null)
            {
                return NotFound();
            }
            dbContext.Students.Remove(std);
            await dbContext.SaveChangesAsync();
            return Ok();

        }
    }
}
