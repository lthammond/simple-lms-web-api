using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleLMSWebApi.Models;

namespace SimpleLMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CourseController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllCourses")]
        public IEnumerable<Course> Index()
        {
            return _context.Courses.ToList();
        }

        [HttpGet("GetCourse")]
        public Course GetCourse(int courseId)
        {
            return _context.Courses.Find(courseId);
        }

        [HttpPost("AddCourse")]
        public IActionResult AddCourse(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("UpdateCourse")]
        public IActionResult UpdateCourse(int oldCourseId, Course newCourse)
        {
            var course = _context.Courses.Find(oldCourseId);
            if (course == null)
            {
                return NotFound();
            }
            course.Name = newCourse.Name;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("RemoveCourse")]
        public IActionResult RemoveCourse(int courseId)
        {
            var course = _context.Courses.Find(courseId);
            if (course == null)
            {
                return NotFound();
            }
            _context.Courses.Remove(course);
            _context.SaveChanges();
            return Ok();
        }
    }
}
