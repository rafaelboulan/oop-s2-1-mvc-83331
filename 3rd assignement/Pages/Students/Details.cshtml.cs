using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _3rd_assignement.Data;
using _3rd_assignement.Models;

namespace _3rd_assignement.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly _3rd_assignement.Data.ApplicationDbContext _context;

        public DetailsModel(_3rd_assignement.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public int TotalCredits { get; set; }

        public Student Student { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            // 👉 IMPORTANT : Include pour charger les relations
            Student = await _context.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (Student == null || Student.UserId != userId)
            {
                return NotFound(); // sécurité
            }

            // 👉 maintenant ça marche
            TotalCredits = Student.Enrollments.Sum(e => e.Course.Credits);

            return Page();
        }
    }
}
