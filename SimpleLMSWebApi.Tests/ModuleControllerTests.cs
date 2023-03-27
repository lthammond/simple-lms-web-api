namespace SimpleLMSWebApi.Tests
{
    [TestFixture]
    public class ModuleControllerTests
    {
        private ModuleController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new ModuleController();
        }

        [Test]
        public void Index_ReturnsListOfModules()
        {
            var result = _controller.Index();

            Assert.IsInstanceOf<IEnumerable<Module>>(result);
            Assert.That(result.Count(), Is.EqualTo(3));
        }

        [Test]
        public void GetModule_ReturnsModule()
        {
            int moduleId = 2;
            Module expectedModule = new Module { CourseId = 1, Id = 2, Name = "Area" };

            Module actualModule = _controller.GetModule(moduleId);

            Assert.That(actualModule.CourseId, Is.EqualTo(expectedModule.CourseId));
            Assert.That(actualModule.Id, Is.EqualTo(expectedModule.Id));
            Assert.That(actualModule.Name, Is.EqualTo(expectedModule.Name));
        }

        [Test]
        public void AddModule_AddsModuleToList()
        {
            var moduleToAdd = new Module { CourseId = 1, Id = 4, Name = "Circles" };

            var result = _controller.AddModule(moduleToAdd);

            Assert.Contains(moduleToAdd, (System.Collections.ICollection?)result);
            Assert.That(result.Count(), Is.EqualTo(4));
        }

        [Test]
        public void UpdateModule_UpdatesModule()
        {
            var oldModule = new Module { CourseId = 1, Id = 1, Name = "Math" };
            var newModule = new Module { CourseId = 1, Id = 2, Name = "Rectangles" };

            var result = _controller.UpdateModule(oldModule.Id, newModule);

            Assert.Contains(newModule, (System.Collections.ICollection?)result);
            Assert.That(result.FirstOrDefault(module => module.Id == oldModule.Id), Is.Not.EqualTo(oldModule));
        }

        [Test]
        public void RemoveModule_RemovesExistingModule()
        {
            var moduleIdToRemove = 3;

            var result = _controller.RemoveModule(moduleIdToRemove);

            Assert.IsNull(result.FirstOrDefault(module => module.Id == moduleIdToRemove));
            Assert.That(result.Count(), Is.EqualTo(2));
        }
    }
}
