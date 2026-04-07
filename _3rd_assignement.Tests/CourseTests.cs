using Xunit;
using _3rd_assignement.Models;

namespace _3rd_assignement.Tests
{
    public class CourseTests
    {
        [Fact]
        public void Course_Should_Set_Title()
        {
            var course = new Course
            {
                Title = "Math"
            };

            Assert.Equal("Math", course.Title);
        }

        [Fact]
        public void Course_Should_Set_Credits()
        {
            var course = new Course
            {
                Credits = 5
            };

            Assert.Equal(5, course.Credits);
        }
    }
}