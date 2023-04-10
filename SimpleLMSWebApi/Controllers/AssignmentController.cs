using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleLMSWebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace SimpleLMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : Controller
    {
        private readonly DatabaseContext _context;

        public AssignmentController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllAssignments")]
        public IEnumerable<Assignment> Index()
        {
            return _context.Assignments.ToList();
        }

        [HttpGet("GetAssignmentsUnderModule")]
        public IEnumerable<Assignment> GetAssignmentsUnderModule(int moduleId)
        {
            return _context.Assignments.Where(m => m.ModuleId == moduleId).ToList();
        }

        [HttpGet("GetAssignment")]
        public Assignment GetAssignment(int assignmentId)
        {
            return _context.Assignments.Find(assignmentId);
        }

        [HttpPost("AddAssignment")]
        public IEnumerable<Assignment> AddAssignment(Assignment assignment)
        {
            _context.Assignments.Add(assignment);
            _context.SaveChanges();
            return _context.Assignments.ToList();
        }

        [HttpPut("UpdateAssignment")]
        public IEnumerable<Assignment> UpdateAssignment(int oldAssignmentId, Assignment newAssignment)
        {
            var assignment = _context.Assignments.Find(oldAssignmentId);
            if (assignment != null)
            {
                assignment.ModuleId = newAssignment.ModuleId;
                assignment.Name = newAssignment.Name;
                assignment.Grade = newAssignment.Grade;
                assignment.DueDate = newAssignment.DueDate;
                _context.SaveChanges();
            }
            return _context.Assignments.ToList();
        }

        [HttpDelete("RemoveAssignment")]
        public IEnumerable<Assignment> RemoveAssignment(int assignmentId)
        {
            var assignment = _context.Assignments.Find(assignmentId);
            if (assignment != null)
            {
                _context.Assignments.Remove(assignment);
                _context.SaveChanges();
            }
            return _context.Assignments.ToList();
        }
    }
}