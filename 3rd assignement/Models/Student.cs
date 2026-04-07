using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _3rd_assignement.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

     
        public string UserId { get; set; } = string.Empty;

        // navigation
        public ICollection<Enrollment> Enrollments { get; set; }
            = new List<Enrollment>();
    }
}