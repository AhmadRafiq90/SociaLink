using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.DBcontext;
using WebApplication1.LoginInstance;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Pages
{
    public class view_joined_societiesModel : PageModel
    {
        public List<Society>? societyList { get; set; }
        public List<SocietyMembership>? Membership { get; set; }
        public string ErrorMessage { get; set; }
        public void OnGet(string query)
        {
            FASTSocietyManagementContextFactory contextFactory = new();
            SocietyService societyService = new(contextFactory.GetContext());
            
            Membership = contextFactory.GetContext().SocietyMemberships.Where(m => m.UserId == Userinstance.id).ToList();

            societyList = (from society in contextFactory.GetContext().Societies
                                 join membership in contextFactory.GetContext().SocietyMemberships
                                 on society.Id equals membership.SocietyId
                                 where membership.UserId == Userinstance.id
                                 select society).ToList();

            if (!string.IsNullOrEmpty(query))
            {
                societyList = societyList.Where(Society =>
                    Society.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    Society.ContactEmail.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }
        public async Task<IActionResult> OnPostLeaveSocietyAsync()
        {
            FASTSocietyManagementContextFactory contextFactory = new();
            string? societyId = Request.Form["societyId"];
            SocietyMembership? societyMembership = contextFactory.GetContext().SocietyMemberships.FirstOrDefault(m => m.UserId == Userinstance.id && m.SocietyId == societyId);
            if (societyMembership != null)
            {
                contextFactory.GetContext().SocietyMemberships.Remove(societyMembership);
                await contextFactory.GetContext().SaveChangesAsync();
            }
            return RedirectToPage("/view_joined_societies");
        }
    }
}
