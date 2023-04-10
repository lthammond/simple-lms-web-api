using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleLMSWebApi.Models;

namespace SimpleLMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : Controller
    {
        private readonly DatabaseContext _context;

        public ModuleController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllModules")]
        public IEnumerable<Module> Index()
        {
            return _context.Modules.ToList();
        }

        [HttpGet("GetModulesUnderCourse")]
        public IEnumerable<Module> GetModulesUnderCourse(int courseId)
        {
            return _context.Modules.Where(m => m.CourseId == courseId).ToList();
        }

        [HttpGet("GetModule")]
        public Module GetModule(int moduleId)
        {
            return _context.Modules.Find(moduleId);
        }

        [HttpPost("AddModule")]
        public IEnumerable<Module> AddModule(Module module)
        {
            _context.Modules.Add(module);
            _context.SaveChanges();
            return _context.Modules.ToList();
        }

        [HttpPut("UpdateModule")]
        public IEnumerable<Module> UpdateModule(int oldModuleId, Module newModule)
        {
            var module = _context.Modules.Find(oldModuleId);
            if (module != null)
            {
                module.Name = newModule.Name;
                _context.SaveChanges();
            }
            return _context.Modules.ToList();
        }

        [HttpDelete("RemoveModule")]
        public IEnumerable<Module> RemoveModule(int moduleId)
        {
            var module = _context.Modules.Find(moduleId);
            if (module != null)
            {
                _context.Modules.Remove(module);
                _context.SaveChanges();
            }
            return _context.Modules.ToList();
        }
    }
}
