using Xunit;
using _3rd_assignement.Models;

namespace _3rd_assignement.Tests
{
    public class EnrollmentTests
    {
        [Fact]
        public void Enrollment_Should_Set_Default_Status()
        {
            // Arrange
            var enrollment = new Enrollment();

            // Act
            var status = enrollment.Status;

            // Assert
            Assert.Equal(EnrollmentStatus.Pending, status);
        }

        [Fact]
        public void Enrollment_Should_Assign_Grade()
        {
            // Arrange
            var enrollment = new Enrollment();

            // Act
            enrollment.Grade = 15;

            // Assert
            Assert.Equal(15, enrollment.Grade);
        }

        [Fact]
        public void Enrollment_Should_Have_StudentId()
        {
            var enrollment = new Enrollment
            {
                StudentId = 1
            };

            Assert.Equal(1, enrollment.StudentId);
        }

        [Fact]
        public void Enrollment_Should_Have_CourseId()
        {
            var enrollment = new Enrollment
            {
                CourseId = 2
            };

            Assert.Equal(2, enrollment.CourseId);
        }
    }
}