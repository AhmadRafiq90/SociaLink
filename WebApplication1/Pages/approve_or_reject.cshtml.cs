using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Services;
using System.Collections.Generic;
using WebApplication1.DBcontext;
namespace WebApplication1.Pages
{
    public class approve_or_rejectModel : PageModel
    {
        public List<Society>? Societies { get; set; }
        public void OnGet()
        {
            // Fetch data from your source (e.g., database)
            // For demonstration, using sample data
            FASTSocietyManagementContextFactory factory = new FASTSocietyManagementContextFactory();
            SocietyService sc = new SocietyService(factory.GetContext());
            Societies = sc.GetSocieties().Where(s => s.IsApproved != 2).ToList();
            //Societies = new List<Society> { new Society { Id = "1", Name = "Society 1", Description = "Description 1" }};
        }

        public async Task<IActionResult> OnPostApproveAsync(string id)
        {
            //Console.WriteLine("Approve society with id: " + id);
            FASTSocietyManagementContextFactory factory = new FASTSocietyManagementContextFactory();
            var _context = factory.GetContext();
            var society = await _context.Societies.FindAsync(id);

            if (society != null)
            {
                society.IsApproved = 2;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./approve_or_reject");
        }


        public async Task<IActionResult> OnPostRejectAsync(string id)
        {
            FASTSocietyManagementContextFactory factory = new FASTSocietyManagementContextFactory();
            var _context = factory.GetContext();
            var society = await _context.Societies.FindAsync(id);

            if (society != null)
            {
                society.IsApproved = 1;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./approve_or_reject");
        }
    }
}
