using Microsoft.EntityFrameworkCore;
using SimpleLMSWebApi.Controllers;
using SimpleLMSWebApi.Models;

namespace SimpleLMSWebApi.Tests
{
    [TestFixture]
    public class PersistenceTests
    {
        private DatabaseContext _dbContext;
        private CourseController _courseController;
        private ModuleController _moduleController;
        private AssignmentController _assignmentController;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlite("DataSource=lmsDb.db")
                .Options;

            _dbContext = new DatabaseContext(options);
            _dbContext.Database.EnsureCreated();

            _courseController = new CourseController(_dbContext);
            _moduleController = new ModuleController(_dbContext);
            _assignmentController = new AssignmentController(_dbContext);
        }

        [Test]
        public void Create_and_read_course()
        {
            var courseToCreate = new Course
            {
                Id = 1,
                Name = "Test Course",
            };

            _courseController.AddCourse(courseToCreate);

            var retrievedCourse = _courseController.GetCourse(courseToCreate.Id);

            Assert.That(retrievedCourse, Is.Not.Null);
            Assert.That(retrievedCourse.Id, Is.EqualTo(courseToCreate.Id));
            Assert.That(retrievedCourse.Name, Is.EqualTo(courseToCreate.Name));
        }

        [Test]
        public void Create_and_read_course_with_modules()
        {
            var courseToCreate = new Course
            {
                Id = 1,
                Name = "Test Course",
            };

            var module1 = new Module
            {
                Id = 1,
                Name = "Test Module 1",
                CourseId = courseToCreate.Id
            };

            var module2 = new Module
            {
                Id = 2,
                Name = "Test Module 2",
                CourseId = courseToCreate.Id
            };

            _courseController.AddCourse(courseToCreate);
            _moduleController.AddModule(module1);
            _moduleController.AddModule(module2);

            var retrievedCourse = _courseController.GetCourse(courseToCreate.Id);
            var retrievedModules = _moduleController.GetModulesUnderCourse(courseToCreate.Id);

            Assert.That(retrievedCourse.Id, Is.EqualTo(courseToCreate.Id));
            Assert.That(retrievedCourse.Name, Is.EqualTo(courseToCreate.Name));

            foreach (var module in retrievedModules)
            {
                Assert.That(module.CourseId, Is.EqualTo(courseToCreate.Id));
            }
        }

        [Test]
        public void Create_and_read_three_courses()
        {
            var course1 = new Course { Id = 1, Name = "Course 1" };
            var course2 = new Course { Id = 2, Name = "Course 2" };
            var course3 = new Course { Id = 3, Name = "Course 3" };

            _courseController.AddCourse(course1);
            _courseController.AddCourse(course2);
            _courseController.AddCourse(course3);

            var retrievedCourses = _courseController.Index();

            Assert.That(retrievedCourses.Count(), Is.EqualTo(3));
            Assert.That(retrievedCourses.Any(course => course.Id == course1.Id), Is.True);
            Assert.That(retrievedCourses.Any(course => course.Id == course2.Id), Is.True);
            Assert.That(retrievedCourses.Any(course => course.Id == course3.Id), Is.True);
        }

        [Test]
        public void Create_assignments_delete_one()
        {
            var assignment1 = new Assignment
            {
                Id = 1,
                Name = "Assignment 1",
                Grade = 90,
                DueDate = DateTime.Now.AddDays(7)
            };

            var assignment2 = new Assignment
            {
                Id = 2,
                Name = "Assignment 2",
                Grade = 95,
                DueDate = DateTime.Now.AddDays(14)
            };

            var assignment3 = new Assignment
            {
                Id = 3,
                Name = "Assignment 3",
                Grade = 85,
                DueDate = DateTime.Now.AddDays(21)
            };

            _assignmentController.AddAssignment(assignment1);
            _assignmentController.AddAssignment(assignment2);
            _assignmentController.AddAssignment(assignment3);
            _assignmentController.RemoveAssignment(assignment2.Id);

            var retrievedAssignments = _assignmentController.Index();

            Assert.That(retrievedAssignments.Count(), Is.EqualTo(2));
            Assert.That(retrievedAssignments.FirstOrDefault(a => a.Id == assignment1.Id), Is.Not.Null);
            Assert.That(retrievedAssignments.FirstOrDefault(a => a.Id == assignment2.Id), Is.Null);
            Assert.That(retrievedAssignments.FirstOrDefault(a => a.Id == assignment3.Id), Is.Not.Null);
        }

        //test 1
        [Test]
        public void Create_assignment_then_update()
        {
            var assignmentToCreate = new Assignment
            {
                Id = 1,
                Name = "Assignment 1",
                Grade = 70,
                DueDate = DateTime.Now
            };

            var updatedAssignment = new Assignment
            {
                Id = 1,
                Name = "Assignment 1",
                Grade = 80,
                DueDate = DateTime.Now
            };

            _assignmentController.AddAssignment(assignmentToCreate);
            _assignmentController.UpdateAssignment(1, updatedAssignment);

            var retrievedAssignment = _assignmentController.GetAssignment(1);

            Assert.That(retrievedAssignment.Id, Is.EqualTo(assignmentToCreate.Id));
            Assert.That(retrievedAssignment.Grade, Is.Not.EqualTo(70));
            Assert.That(retrievedAssignment.Grade, Is.EqualTo(80));
        }

        //test 2
        [Test]
        public void Remove_module()
        {
            var module1 = new Module
            {
                Id = 1,
                Name = "Test Module 1"
            };

            _moduleController.AddModule(module1);
            _moduleController.RemoveModule(1);

            var retrievedModules = _moduleController.Index();

            Assert.That(retrievedModules.Count, Is.EqualTo(0));
        }

        //test 3
        [Test]
        public void Retrieve_Course_Name_From_Assignment()
        {
            var courseToCreate = new Course
            {
                Id = 1,
                Name = "Geometry",
            };

            var moduleToCreate = new Module
            {
                Id = 1,
                Name = "Volume",
                CourseId = courseToCreate.Id
            };

            var assignmentToCreate = new Assignment
            {
                Id = 1,
                Name = "Formula_HW_1",
                Grade = 70,
                DueDate = DateTime.Now.AddDays(25),
                ModuleId = moduleToCreate.Id
            };

            _courseController.AddCourse(courseToCreate);
            _moduleController.AddModule(moduleToCreate);
            _assignmentController.AddAssignment(assignmentToCreate);

            var retrievedAssignment = _assignmentController.GetAssignment(assignmentToCreate.Id);
            var retrievedModule = _moduleController.GetModule(retrievedAssignment.ModuleId);
            var retrievedCourse = _courseController.GetCourse(retrievedModule.CourseId);

            Assert.That(retrievedCourse.Name, Is.EqualTo(courseToCreate.Name));
        }


        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
        }
    }
}
