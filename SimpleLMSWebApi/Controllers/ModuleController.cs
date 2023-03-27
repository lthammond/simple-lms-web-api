using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace SimpleLMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : Controller
    {
        List<Module> modules = new List<Module> {
            new Module { CourseId = 1, Id = 1, Name = "Perimeter" },
            new Module { CourseId = 1, Id = 2, Name = "Area" },
            new Module { CourseId = 1, Id= 3, Name = "Volume"}
        };

        [HttpGet("GetModules")]
        public IEnumerable<Module> Index()
        {
            return modules;
        }

        [HttpGet("GetModule")]
        public Module GetModule(int moduleId)
        {
            return modules.Find(module => module.Id == moduleId);
        }

        [HttpPost("AddModule")]
        public IEnumerable<Module> AddModule(Module module)
        {
            modules.Add(module);
            return modules;
        }

        [HttpPut("UpdateModule")]
        public IEnumerable<Module> UpdateModule(int oldModuleId, Module newModule)
        {
            int index = modules.FindIndex(course => course.Id == oldModuleId);
            modules[index] = newModule;
            return modules;
        }

        [HttpDelete("RemoveModule")]
        public IEnumerable<Module> RemoveModule(int moduleId)
        {
            modules.RemoveAll(module => module.Id == moduleId);
            return modules;
        }
    }
}