using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Web.Mvc.Controls;
using WebApplication1.DBcontext;
using WebApplication1.Models;
using System;
using System.Web;
using Microsoft.Identity.Client;
using WebApplication1.Hasher;
using WebApplication1.Services;
using Microsoft.IdentityModel.Tokens;

namespace WebApplication1.Pages
{
	public class edit_userModel : PageModel
    {
        public User? user { get; set; }
        public bool isAdmin { get; set; }
        public bool isStudent { get; set; }
        public bool isHead { get; set; }

		public void OnGet(string id)
        {
           // Console.WriteLine(id);
            // get user by id using database context.
            FASTSocietyManagementContextFactory factory = new FASTSocietyManagementContextFactory();
            user = factory.GetContext().Users.Find(id);
            if (user != null)
            {
                if (user.Role == "Admin")
                {
					isAdmin = true;
				}
				else if (user.Role == "Student")
                {
					isStudent = true;
				}
				else if (user.Role == "SocietyHead")
                {
					isHead = true;
				}
            }
            else
            {
                Console.WriteLine("User not found");
            }
		}
        public void OnPostUpdate(string id)
        {
			FASTSocietyManagementContextFactory factory = new FASTSocietyManagementContextFactory();
			user = factory.GetContext().Users.Find(id);

            user.FirstName = Request.Form["firstName"];
            user.LastName = Request.Form["lastName"];
            user.Email = Request.Form["email"];
            user.Role = Request.Form["role"];

            Console.WriteLine(Request.Form["firstName"]);
            Console.WriteLine(Request.Form["lastName"]);

            string password = Request.Form["newpass1"];
            string confirmPassword = Request.Form["newpass2"];
            if (!password.IsNullOrEmpty() && password == confirmPassword)
            {
				user.PasswordHash = SecurePasswordHasher.Hash(password);
			}
            // update user in database.
            factory.GetContext().Users.Update(user);
            factory.GetContext().SaveChanges();

            Response.Redirect("/ManageUsers");
        }
    }
}
