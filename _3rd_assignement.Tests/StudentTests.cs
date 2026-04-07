using Xunit;
using _3rd_assignement.Models;

namespace _3rd_assignement.Tests
{
    public class StudentTests
    {
        [Fact]
        public void Student_Should_Set_UserId()
        {
            var student = new Student
            {
                UserId = "abc123"
            };

            Assert.Equal("abc123", student.UserId);
        }
    }
}