using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DBcontext;

namespace WebApplication1.Pages
{
    public class ViewEventsModel : PageModel
    {
        private readonly FASTSocietyManagementContext _context;

        public ViewEventsModel(FASTSocietyManagementContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public DateTime SelectedDate { get; set; }

        public IList<Event> Events { get; set; }

        public async Task OnGetAsync()
        {
            if (SelectedDate == DateTime.MinValue)
            {
                SelectedDate = DateTime.Today;
            }

            Events = await _context.Events
                .Where(e => e.IsApproved == 1 && e.EventDate.Date == SelectedDate.Date)
                .ToListAsync();
        }
    }
}
