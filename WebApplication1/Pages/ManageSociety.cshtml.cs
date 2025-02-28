using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.DBcontext;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Pages
{
    public class ManageSocietyModel : PageModel
    {
        public List<Society>? societyList;
        public void OnGet(string query)
        {
            FASTSocietyManagementContextFactory contextFactory = new();
            SocietyService societyService = new(contextFactory.GetContext());
            societyList = societyService.GetSocieties();

            if (!string.IsNullOrEmpty(query))
            {
                societyList = societyList.Where(Society =>
                    Society.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    Society.ContactEmail.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }
        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            Console.WriteLine("Delete society with id: " + id);
            FASTSocietyManagementContextFactory factory = new FASTSocietyManagementContextFactory();
            var _context = factory.GetContext();
            var society = await _context.Societies.FindAsync(id);

            if (society != null)
            {
                _context.Societies.Remove(society);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./ManageSociety");
        }
    }
}
