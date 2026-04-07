using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _3rd_assignement.Data;
using _3rd_assignement.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _3rd_assignement.Pages.Enrollments
{
    public class EditModel : PageModel
    {
        private readonly _3rd_assignement.Data.ApplicationDbContext _context;

        public EditModel(_3rd_assignement.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Enrollment Enrollment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment =  await _context.Enrollments.FirstOrDefaultAsync(m => m.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }
            Enrollment = enrollment;
           ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id");
            ViewData["StudentId"] = _context.Students
    .Join(_context.Users,
        s => s.UserId,
        u => u.Id,
        (s, u) => new SelectListItem
        {
            Value = s.Id.ToString(), 
            Text = u.Email           
        })
    .ToList();
            ViewData["StatusList"] = Enum.GetValues(typeof(EnrollmentStatus))
    .Cast<EnrollmentStatus>()
    .Select(e => new SelectListItem
    {
        Value = e.ToString(),
        Text = e.ToString()
    })
    .ToList();
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Enrollment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrollmentExists(Enrollment.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollments.Any(e => e.Id == id);
        }
    }
}
