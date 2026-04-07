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
    public class IndexModel : PageModel
    {
        private readonly _3rd_assignement.Data.ApplicationDbContext _context;

        public IndexModel(_3rd_assignement.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            Student= await _context.Students
                .Where(s => s.UserId == userId)
                .ToListAsync();
        }
    }
}
