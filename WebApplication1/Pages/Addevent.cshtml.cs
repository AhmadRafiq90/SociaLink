using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DBcontext;
using Google;

namespace WebApplication1.Pages
{
    public class AddEventModel : PageModel
    {
        private readonly FASTSocietyManagementContext _context; 

        public AddEventModel(FASTSocietyManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Event EventInfo { get; set; }

        public SelectList SocietyOptions { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            SocietyOptions = new SelectList(await _context.Societies
                .Where(s => s.IsApproved == 2)
                .ToListAsync(), "Name", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    PopulateSocietyDropdown();
            //    return Page();
            //}


            
            var society = await _context.Societies
                            .FirstOrDefaultAsync(s => s.Name == EventInfo.societyName && s.IsApproved == 2);


            if (society != null)
            {
                
               EventInfo.SocietyId = society.Id;

                _context.Events.Add(EventInfo);
                await _context.SaveChangesAsync();

                return RedirectToPage("./society_dashboard"); // Redirect to the correct success page.
            }
            else
            {
             
              ModelState.AddModelError("EventInfo.SocietyName", "The selected society does not exist or is not approved.");
               PopulateSocietyDropdown();
                return Page();
            }
        }

        private void PopulateSocietyDropdown()
        {
            SocietyOptions = new SelectList(_context.Societies.Where(s => s.IsApproved == 2), "Name", "Name");
        }

    }
}
