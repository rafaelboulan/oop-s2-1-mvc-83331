using Xunit;
using _3rd_assignement.Models;

namespace _3rd_assignement.Tests
{
    public class EnrollmentLogicTests
    {
        [Fact]
        public void New_Enrollment_Should_Have_Default_Status_Pending()
        {
            var enrollment = new Enrollment();

            Assert.Equal(EnrollmentStatus.Pending, enrollment.Status);
        }

        [Fact]
        public void Enrollment_Can_Change_Status_To_Approved()
        {
            var enrollment = new Enrollment();

            enrollment.Status = EnrollmentStatus.Approved;

            Assert.Equal(EnrollmentStatus.Approved, enrollment.Status);
        }

        [Fact]
        public void Enrollment_Can_Change_Status_To_Rejected()
        {
            var enrollment = new Enrollment();

            enrollment.Status = EnrollmentStatus.Rejected;

            Assert.Equal(EnrollmentStatus.Rejected, enrollment.Status);
        }

        [Fact]
        public void Enrollment_Should_Link_Student_And_Course()
        {
            var enrollment = new Enrollment
            {
                StudentId = 1,
                CourseId = 2
            };

            Assert.Equal(1, enrollment.StudentId);
            Assert.Equal(2, enrollment.CourseId);
        }

        [Fact]
        public void Enrollment_With_Grade_Should_Store_Value()
        {
            var enrollment = new Enrollment
            {
                Grade = 18
            };

            Assert.True(enrollment.Grade >= 0);
        }
    }
}