using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace SimpleLMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : Controller
    {
        List<Assignment> assignments = new List<Assignment> {
            new Assignment { ModuleId = 1, Id = 1, Name = "W1-A1", Grade = 95, DueDate = DateTime.Now},
            new Assignment { ModuleId = 1, Id = 2, Name = "W1-A2", Grade = 100, DueDate = DateTime.Now},
            new Assignment { ModuleId = 2, Id = 3, Name = "W2-A1", Grade = 91, DueDate = DateTime.Now},
            new Assignment { ModuleId = 2, Id = 4, Name = "W2-A2", Grade = 79, DueDate = DateTime.Now},
            new Assignment { ModuleId = 3, Id = 5, Name = "Exam-1", Grade = 85, DueDate = DateTime.Now}
        };

        [HttpGet("GetAssignments")]
        public IEnumerable<Assignment> Index()
        {
            return assignments;
        }

        [HttpGet("GetAssignment")]
        public Assignment GetAssignment(int assignmentId)
        {
            return assignments.Find(assignment => assignment.Id == assignmentId);
        }

        [HttpPost("AddAssignment")]
        public IEnumerable<Assignment> AddAssignment(Assignment assignment)
        {
            assignments.Add(assignment);
            return assignments;
        }

        [HttpPut("UpdateAssignment")]
        public IEnumerable<Assignment> UpdateAssignment(int oldAssignmentId, Assignment newAssignment)
        {
            int index = assignments.FindIndex(assignment => assignment.Id == oldAssignmentId);
            assignments[index] = newAssignment;
            return assignments;
        }

        [HttpDelete("RemoveAssignment")]
        public IEnumerable<Assignment> RemoveAssignment(int assignmentId)
        {
            assignments.RemoveAll(assignment => assignment.Id == assignmentId);
            return assignments;
        }
    }
}