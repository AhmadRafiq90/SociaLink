using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.DBcontext;

namespace WebApplication1.Pages
{
    public class ApproveEventModel : PageModel
    {
        private readonly FASTSocietyManagementContext _context;

        public ApproveEventModel(FASTSocietyManagementContext context)
        {
            _context = context;
        }

        public IList<Event> Events { get; set; }

        public async Task OnGetAsync()
        {
            Events = await _context.Events
                .Where(e => e.IsApproved == 0) //Fetch only the events that are not approved
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostApproveAsync(int id)
        {
            var eventItem = await _context.Events.FindAsync(id);
            if (eventItem != null)
            {
                eventItem.IsApproved = 1; // Set to 1 to indicate approval
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRejectAsync(int id)
        {
            var eventItem = await _context.Events.FindAsync(id);
            if (eventItem != null)
            {
                eventItem.IsApproved = -1; // Set to -1 to indicate rejection
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
