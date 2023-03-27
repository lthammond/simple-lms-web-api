using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace SimpleLMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : Controller
    {
        List<Course> courses = new List<Course> {
            new Course { Id = 1, Name = "Math"},
            new Course { Id = 2, Name = "History"},
            new Course { Id = 3, Name = "Science"},
            new Course { Id = 4, Name = "English"}
        };

        [HttpGet("GetCourses")]
        public IEnumerable<Course> Index()
        {
            return courses;
        }

        [HttpGet("GetCourse")]
        public Course GetCourse(int courseId)
        {
            return courses.Find(course => course.Id == courseId);
        }

        [HttpPost("AddCourse")]
        public IEnumerable<Course> AddCourse(Course course)
        {
            courses.Add(course);
            return courses;
        }

        [HttpPut("UpdateCourse")]
        public IEnumerable<Course> UpdateCourse(int oldCourseId, Course newCourse)
        {
            int index = courses.FindIndex(course => course.Id == oldCourseId);
            courses[index] = newCourse;
            return courses;
        }

        [HttpDelete("RemoveCourse")]
        public IEnumerable<Course> RemoveCourse(int courseId)
        {
            courses.RemoveAll(course => course.Id == courseId);
            return courses;
        }
    }
}
