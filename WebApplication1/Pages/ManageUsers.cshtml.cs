using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.DBcontext;
using WebApplication1.Models;
using WebApplication1.Services;
namespace WebApplication1.Pages
{
    public class ManageUsersModel : PageModel
    {
        public List<User>? Users { get; set; }
        public void OnGet(string query)
        {
            Console.WriteLine("get called");
            // Fetch data from your source (e.g., database)
            // For demonstration, using sample data
            FASTSocietyManagementContextFactory factory = new FASTSocietyManagementContextFactory();
            UserService us = new UserService(factory.GetContext());
            Users = us.GetUsers();

            if (!string.IsNullOrEmpty(query))
            {
                Users = Users.Where(user =>
                    user.Email.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    user.FirstName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    user.LastName.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }


        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            Console.WriteLine("Delete user with id: " + id);
            FASTSocietyManagementContextFactory factory = new FASTSocietyManagementContextFactory();
            var _context = factory.GetContext();
            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./ManageUsers");
        }

    }
}
