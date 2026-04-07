using Xunit;
using _3rd_assignement.Models;

namespace _3rd_assignement.Tests
{
    public class EnrollmentStatusTests
    {
        [Fact]
        public void Status_Should_Be_Pending()
        {
            var status = EnrollmentStatus.Pending;
            Assert.Equal(EnrollmentStatus.Pending, status);
        }

        [Fact]
        public void Status_Should_Be_Approved()
        {
            var status = EnrollmentStatus.Approved;
            Assert.Equal(EnrollmentStatus.Approved, status);
        }

        [Fact]
        public void Status_Should_Be_Rejected()
        {
            var status = EnrollmentStatus.Rejected;
            Assert.Equal(EnrollmentStatus.Rejected, status);
        }
    }
}