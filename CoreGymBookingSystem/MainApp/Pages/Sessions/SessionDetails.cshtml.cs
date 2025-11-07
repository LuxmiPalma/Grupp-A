using DAL.DbContext;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MainApp.Pages.Sessions
{
    public class SessionDetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public SessionDetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Session Session { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var session = await _context.Sessions
                .Include(s => s.Instructor)
                .Include(s => s.Bookings)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (session == null)
            {
                return NotFound();
            }

            Session = session;
            return Page();
        }
    }
}
  
