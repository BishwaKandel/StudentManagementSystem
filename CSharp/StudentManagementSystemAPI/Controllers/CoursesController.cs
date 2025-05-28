using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemAPI.Models;
using StudentManagementSystemAPI.Data;

namespace StudentManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CoursesController(AppDbContext context)
        {
            _context = context;
        }

        // Create Course 

        [HttpPost]

        public IActionResult CreateCourse(Course course) 
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
            return Ok(course);
        }


        //Get Course by ID 

        [HttpGet("{id}")]

        public IActionResult GetCourseByID(int id )
        {
            var course = _context.Courses.Find(id);
            if (course == null)
            {
                return NotFound($"The course of ID {id} is not found.");
            }

            else return Ok(course);
        }

        //Get All Courses 

        [HttpGet]

        public IActionResult GetAllCourses()
        {
            var courses = _context.Courses.ToList();
            return Ok(courses);
        }

        //Update Courses Details

        [HttpPut("{id}")]

        public IActionResult UpdateCourses(int id , [FromBody] Course updatedCourse)
        {
            var CourseinDb = _context.Courses.Find(id);

            if (CourseinDb == null)
            {
                return NotFound($"The course with id {id} is not found");
            }

            else
            {
                CourseinDb.Name = updatedCourse.Name;
                CourseinDb.Description = updatedCourse.Description;
            }

            _context.SaveChanges();
            return Ok(CourseinDb);
        }

        //Delete Course

        [HttpDelete("{id}")]

        public IActionResult DeleteCourses(int id)
        {
            var CourseinDb = _context.Courses.Find(id);

            if (CourseinDb == null)
            {
                return NotFound($"The course with Course ID {id} is not found");
            }
            else _context.Courses.Remove(CourseinDb);

            _context.SaveChanges();
            return Ok(CourseinDb);
        }

        
    }
}
