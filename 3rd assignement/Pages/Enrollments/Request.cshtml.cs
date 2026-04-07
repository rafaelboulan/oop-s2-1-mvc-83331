using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using _3rd_assignement.Data;
using _3rd_assignement.Models;

namespace _3rd_assignement.Pages.Enrollments
{
    public class RequestModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public RequestModel(ApplicationDbContext context,
                            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Course> Courses { get; set; } = new();

        [BindProperty]
        public int CourseId { get; set; }

        public async Task OnGetAsync()
        {
            Courses = await _context.Courses.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Unauthorized();
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.UserId == user.Id);

            if (student == null)
                return Unauthorized();

            // =========================
            // ANTI DOUBLON
            // =========================
            var exists = await _context.Enrollments
                .AnyAsync(e => e.CourseId == CourseId
                            && e.StudentId == student.Id);

            if (exists)
            {
                ModelState.AddModelError(string.Empty, "You already requested this course.");
                Courses = await _context.Courses.ToListAsync();
                return Page();
            }

            // =========================
            // CREATE ENROLLMENT
            // =========================
            var enrollment = new Enrollment
            {
                CourseId = CourseId,
                StudentId = student.Id,
                Status = EnrollmentStatus.Pending,
                RequestedAt = System.DateTime.UtcNow
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Enrollments/Index");
        }
    }
}