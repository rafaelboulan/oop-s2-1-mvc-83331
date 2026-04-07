using System;
using System.ComponentModel.DataAnnotations;

namespace _3rd_assignement.Models
{
    public enum EnrollmentStatus
    {
        Pending,
        Approved,
        Rejected
    }

    public class Enrollment
    {
        public int Id { get; set; }

        // 🔥 doit matcher Student.Id (int)
        public int StudentId { get; set; }

        public Student? Student { get; set; }

        public int CourseId { get; set; }

        public Course? Course { get; set; }

        public int? Grade { get; set; }

        public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Pending;

        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
    }
}