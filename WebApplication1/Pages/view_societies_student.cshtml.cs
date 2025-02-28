using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using WebApplication1.DBcontext;
using WebApplication1.Services;
using WebApplication1.Models;
using Microsoft.AspNetCore.Identity;
using WebApplication1.LoginInstance;

namespace WebApplication1.Pages
{
    public class view_societies_studentModel : PageModel
    {
        public List<Society>? societyList;
        public int[] memberCounts = Array.Empty<int>();
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public void OnGet(string query)
        {
            FASTSocietyManagementContextFactory contextFactory = new();
            SocietyService societyService = new(contextFactory.GetContext());
            societyList = societyService.GetSocieties();
            //societyList = societyList.Where(Society => Society.IsApproved == true).ToList();

            memberCounts = new int[societyList.Count];
            for (int i = 0; i < societyList.Count; i++)
            {
                memberCounts[i] = (from user in contextFactory.GetContext().Users
                                  join membership in contextFactory.GetContext().SocietyMemberships
                                  on user.Id equals membership.UserId
                                  where membership.SocietyId == societyList[i].Id
                                  select user).Count();
            }

            if (!string.IsNullOrEmpty(query))
            {
                societyList = societyList.Where(Society =>
                    Society.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    Society.ContactEmail.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }


        public async Task<IActionResult> OnPostJoinSocietyAsync(string societyId)
        {
            Console.WriteLine("Joining society with id: " + societyId);
            FASTSocietyManagementContextFactory factory = new FASTSocietyManagementContextFactory();
            var _context = factory.GetContext();
            
            // write query to check if same societyid and userid exist in the table.
            var SocietyMembershipinstance = _context.SocietyMemberships.FirstOrDefault(sm => sm.UserId == Userinstance.id && sm.SocietyId == societyId);
            if (SocietyMembershipinstance != null)
            {
                ErrorMessage = "You are already a member of this society.";
                ModelState.AddModelError(string.Empty, "You are already a member of this society.");
                OnGet("");
                return Page();
            }

            // Add the user to the society
            SocietyMembership sm = new SocietyMembership();
            sm.SocietyId = societyId;
            sm.UserId = Userinstance.id;
            sm.Id = Guid.NewGuid().ToString();
            sm.Role = "Member";
            sm.JoinDate = DateTime.Now;
            sm.isApproved = 0;
            _context.SocietyMemberships.Add(sm);

            await _context.SaveChangesAsync();

            SuccessMessage = "Your request has been sent to the society head for approval.";
            OnGet("");
            // Redirect the user back to the societies page
            return Page();
        }

    }
}
