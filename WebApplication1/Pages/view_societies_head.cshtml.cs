using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.DBcontext;
using WebApplication1.LoginInstance;
using WebApplication1.Models;

namespace WebApplication1.Pages
{
    public class view_societies_headModel : PageModel
    {
        public List<Society>? societyList { get; set; }
        public void OnGet()
        {
            FASTSocietyManagementContextFactory factory = new FASTSocietyManagementContextFactory();
            // select societies where head is the current user

            societyList = (from society in factory.GetContext().Societies
                           join user in factory.GetContext().Users
                           on society.ContactEmail equals Userinstance.email
                           select society).Distinct().ToList();
        }
    }
}
