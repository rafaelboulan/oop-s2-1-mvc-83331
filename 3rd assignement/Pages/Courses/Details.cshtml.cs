using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _3rd_assignement.Data;
using _3rd_assignement.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace _3rd_assignement.Pages.Courses
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Course Course { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var course = await _context.Courses
                .Include(c => c.Enrollments)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (course == null)
                return NotFound();

            Course = course;
            return Page();
        }

        public async Task<IActionResult> OnPostRequestEnrollmentAsync(int courseId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

          
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.UserId == userId);

            if (student == null)
            {
                return Unauthorized();
            }

            var enrollment = new Enrollment
            {
                CourseId = courseId,
                StudentId = student.Id,
                Status = EnrollmentStatus.Pending
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return RedirectToPage(new { id = courseId });
        }
    }
}
