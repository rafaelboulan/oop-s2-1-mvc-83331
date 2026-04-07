using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace _3rd_assignement.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Range(1, 30)]
        public int Credits { get; set; }

        // =========================
        // OWNER (Institution / Admin)
        // =========================
        public string? CreatedByUserId { get; set; }

        public IdentityUser? CreatedByUser { get; set; }

        // =========================
        // NAVIGATION (Enrollments)
        // =========================
        public ICollection<Enrollment> Enrollments { get; set; }
            = new List<Enrollment>();
    }
}