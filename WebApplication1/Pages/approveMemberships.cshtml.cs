using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.DBcontext;
using WebApplication1.LoginInstance;
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.LoginInstance;

namespace WebApplication1.Pages
{
    public class approveMembershipsModel : PageModel
    {
        public List <SocietyMembership>? Memberships { get; set; }
        public List<string>? Names { get; set; }
        public void OnGet()
        {
            FASTSocietyManagementContextFactory factory = new FASTSocietyManagementContextFactory();
            // select societies where head is the current user

            Memberships = (from membership in factory.GetContext().SocietyMemberships
                           join society in factory.GetContext().Societies
                           on membership.SocietyId equals society.Id
                           where society.ContactEmail == Userinstance.email
                           select membership)
                      .ToList();


            Names = (from user in factory.GetContext().Users
                     join membership in factory.GetContext().SocietyMemberships
                     on user.Id equals membership.UserId
                     join society in factory.GetContext().Societies
                     on membership.SocietyId equals society.Id
                     where society.ContactEmail == Userinstance.email
                     select user.FirstName + " " + user.LastName)
                .ToList();
        }
        public async Task<IActionResult> OnPostApproveAsync(string id)
        {
            FASTSocietyManagementContextFactory factory = new FASTSocietyManagementContextFactory();
            var _context = factory.GetContext();
            var membership = await _context.SocietyMemberships.FindAsync(id);

            if (membership != null)
            {
                membership.isApproved = 2;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./approveMembership");
        }

        public async Task<IActionResult> OnPostRejectAsync(string id)
        {
            FASTSocietyManagementContextFactory factory = new FASTSocietyManagementContextFactory();
            var _context = factory.GetContext();
            var membership = await _context.SocietyMemberships.FindAsync(id);

            if (membership != null)
            {
                membership.isApproved = 1;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./approveMembership");
        }
    }
}
