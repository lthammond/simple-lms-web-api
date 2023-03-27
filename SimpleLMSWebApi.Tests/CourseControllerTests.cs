namespace SimpleLMSWebApi.Tests
{
    [TestFixture]
    public class CourseControllerTests
    {
        private CourseController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new CourseController();
        }

        [Test]
        public void Index_ReturnsAllCourses()
        {
            var result = _controller.Index();

            Assert.IsInstanceOf<IEnumerable<Course>>(result);
            Assert.That(result.Count(), Is.EqualTo(4));
        }

        [Test]
        public void GetCourse_ReturnsCourse()
        {
            int courseId = 2;
            Course expectedCourse = new Course { Id = 2, Name = "History" };

            Course actualCourse = _controller.GetCourse(courseId);

            Assert.That(actualCourse.Id, Is.EqualTo(expectedCourse.Id));
            Assert.That(actualCourse.Name, Is.EqualTo(expectedCourse.Name));
        }


        [Test]
        public void AddCourse_AddsCourseToList()
        {
            var newCourse = new Course { Id = 5, Name = "Biology" };

            var result = _controller.AddCourse(newCourse);

            Assert.IsInstanceOf<IEnumerable<Course>>(result);
            Assert.That(result.Count(), Is.EqualTo(5));
            Assert.IsTrue(result.Contains(newCourse));
        }

        [Test]
        public void UpdateCourse_UpdatesCourse()
        {
            var oldCourseId = 2;
            var newCourse = new Course { Id = 2, Name = "Updated History" };

            var result = _controller.UpdateCourse(oldCourseId, newCourse);

            Assert.IsInstanceOf<IEnumerable<Course>>(result);
            Assert.That(result.Count(), Is.EqualTo(4));
            Assert.IsTrue(result.Contains(newCourse));
        }

        [Test]
        public void RemoveCourse_RemovesCourse()
        {
            var courseId = 3;

            var result = _controller.RemoveCourse(courseId);

            Assert.IsInstanceOf<IEnumerable<Course>>(result);
            Assert.That(result.Count(), Is.EqualTo(3));
            Assert.IsFalse(result.Any(c => c.Id == courseId));
        }
    }
}
