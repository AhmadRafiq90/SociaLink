using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using System.Collections.Generic;
using WebApplication1.DBcontext;
using WebApplication1.LoginInstance;

namespace WebApplication1.Pages
{
    public class ViewFeedbackModel : PageModel
    {
        public string SocietyName { get; private set; }
        public string SocietyId { get; private set; }
        public List<Feedback> FeedbackList { get; private set; }
        public IActionResult OnGet(string societyId)
        {
            var factory = new FASTSocietyManagementContextFactory ();
            var context = factory.GetContext();
            if (societyId != null)
            {
                SocietyId = societyId;
                FeedbackList.Add(context.Feedbacks.Find(societyId));
            }
            else
                FeedbackList = context.Feedbacks.ToList();

            return Page();
        }
    }
}
