using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemAPI.Models;
using StudentManagementSystemAPI.Data;

namespace StudentManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        // Create STUDENT
        [HttpPost]
        public IActionResult Create(Student student)
        {
            var existingStudent = _context.Students
                .FirstOrDefault(s => s.Email == student.Email);

            if (existingStudent != null)
            {
                return Conflict($"A student with email '{student.Email}' already exists.");
            }

            _context.Students.Add(student);
            _context.SaveChanges();

            return Ok(student);
        }


        //GET THE STUDENTS BY ID
        [HttpGet("{id}")]

        public IActionResult Get(int id)
        {
            var student = _context.Students.Find(id);

            if (student == null)
            {
                return NotFound($"Student with StudentID {id} not found");
            }

            else return (Ok(student));
        }

        //Get ALl Students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = _context.Students.ToList();
            return Ok(students);
        }

        //Update Student info by ID

        [HttpPut("{id}")]

        public IActionResult updateStudent(int id, [FromBody] Student updatedStudent)
        {
            var studentinDb = _context.Students.Find(id);

            if (studentinDb == null)
            {
                return NotFound($"Student with Student ID {id} is not found");
            }

            else
            {
                studentinDb.FirstName = updatedStudent.FirstName;
                studentinDb.LastName = updatedStudent.LastName;
                studentinDb.Email = updatedStudent.Email;
                studentinDb.DateofBirth = updatedStudent.DateofBirth;
            }

            _context.SaveChanges();
            return Ok(studentinDb);
        }

        //Delete Student 

        [HttpDelete("{id}")]

        public IActionResult DeleteStudent(int id)
        {
            var StudentinDb = _context.Students.Find(id);

            if (StudentinDb == null)
            {
                return NotFound($"Student with StudentID {id} is not found");
            }

            else
            {
                _context.Students.Remove(StudentinDb);
            }

            _context.SaveChanges();
            return Ok(StudentinDb);
        }
    }
}

