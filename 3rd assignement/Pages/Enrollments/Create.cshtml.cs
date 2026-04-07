using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using _3rd_assignement.Data;
using _3rd_assignement.Models;

namespace _3rd_assignement.Pages.Enrollments
{
    public class CreateModel : PageModel
    {
        private readonly _3rd_assignement.Data.ApplicationDbContext _context;

        public CreateModel(_3rd_assignement.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
     
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Title");

            ViewData["StudentId"] = _context.Users
     .Select(u => new SelectListItem
     {
         Value = u.Id,
         Text = u.Email
     })
     .ToList();

            return Page();
        }
          
        

        [BindProperty]
        public Enrollment Enrollment { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Enrollments.Add(Enrollment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
