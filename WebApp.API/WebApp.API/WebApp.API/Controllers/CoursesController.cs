using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.API.CoursesDbContext;
using WebApp.API.Models;

namespace WebApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : Controller
    {
        private readonly CoursesDbContext.CoursesDbContext _dbContext;
        public CoursesController(CoursesDbContext.CoursesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet] 
        public async Task<IActionResult> GetAllCourses()
        {
           var courses = await _dbContext.Courses.ToListAsync();
           return Ok(courses);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetCourse([FromRoute] Guid id)
        {
            var course = await _dbContext.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if(course == null)
                return BadRequest();

            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody]Course course)
        {
            course.Id = Guid.NewGuid();
            await _dbContext.Courses.AddAsync(course);
            await _dbContext.SaveChangesAsync();
            return Ok(course);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateCourse([FromRoute] Guid id, Course request)
        {
            var course = await _dbContext.Courses.FindAsync(id);
            if (course == null)
                return BadRequest();

            course.Title = request.Title;
            course.Description = request.Description;
            course.StartDate = request.StartDate;
            await _dbContext.SaveChangesAsync();

            return Ok(course);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCourse([FromRoute] Guid id)
        {
            var course = await _dbContext.Courses.FindAsync(id);       

            if (course == null)
                return BadRequest();

            _dbContext.Courses.Remove(course);
            await _dbContext.SaveChangesAsync();

            return Ok(course);
        }


    }
}
