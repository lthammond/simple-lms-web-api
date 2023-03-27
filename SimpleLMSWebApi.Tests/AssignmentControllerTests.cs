namespace SimpleLMSWebApi.Tests
{
    [TestFixture]
    public class AssignmentControllerTests
    {
        private AssignmentController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new AssignmentController();
        }

        [Test]
        public void Index_ReturnsListOfAssignments()
        {
            var result = _controller.Index();

            Assert.IsInstanceOf<IEnumerable<Assignment>>(result);
        }

        [Test]
        public void GetAssignment_ReturnsAssignment()
        {
            int assignmentId = 2;
            Assignment expectedAssignment = new Assignment { ModuleId = 1, Id = 2, Name = "W1-A2", Grade = 100, DueDate = DateTime.Now };

            Assignment actualAssignment = _controller.GetAssignment(assignmentId);

            Assert.That(actualAssignment.ModuleId, Is.EqualTo(expectedAssignment.ModuleId));
            Assert.That(actualAssignment.Id, Is.EqualTo(expectedAssignment.Id));
            Assert.That(actualAssignment.Name, Is.EqualTo(expectedAssignment.Name));
            Assert.That(actualAssignment.Grade, Is.EqualTo(expectedAssignment.Grade));
        }

        [Test]
        public void AddAssignment_AddsAssignmentToList()
        {
            var assignmentToAdd = new Assignment { ModuleId = 1, Id = 6, Name = "W3-A1", Grade = 90, DueDate = DateTime.Now.AddDays(7) };

            var result = _controller.AddAssignment(assignmentToAdd);

            Assert.That((System.Collections.ICollection?)result, Does.Contain(assignmentToAdd));
        }

        [Test]
        public void UpdateAssignment_UpdatesAssignment()
        {
            var oldAssignment = new Assignment { ModuleId = 1, Id = 1, Name = "W1-A1", Grade = 98, DueDate = DateTime.Now.AddDays(3) };
            var newAssignment = new Assignment { ModuleId = 1, Id = 2, Name = "W1-A2", Grade = 98, DueDate = DateTime.Now.AddDays(3) };

            var result = _controller.UpdateAssignment(oldAssignment.Id, newAssignment);

            Assert.Contains(newAssignment, (System.Collections.ICollection?)result);
            Assert.That(result.FirstOrDefault(assignment => assignment.Id == oldAssignment.Id), Is.Not.EqualTo(oldAssignment));
        }

        [Test]
        public void RemoveAssignment_RemovesAssignment()
        {
            var assignmentIdToRemove = 3;

            var result = _controller.RemoveAssignment(assignmentIdToRemove);

            Assert.IsNull(result.FirstOrDefault(module => module.Id == assignmentIdToRemove));
        }
    }
}
