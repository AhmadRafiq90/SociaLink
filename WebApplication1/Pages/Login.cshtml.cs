using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using WebApplication1.DBcontext;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Hasher;
using WebApplication1.LoginInstance;

namespace WebApplication1.Pages
{
    public class LoginModel : PageModel
    {
        private readonly FASTSocietyManagementContext _context;

        public LoginModel(FASTSocietyManagementContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Example authentication logic
            var user = _context.Users.FirstOrDefault(u => u.Email == Input.Email); // Ensure the password is hashed

            if (user != null && SecurePasswordHasher.Verify(Input.Password, user.PasswordHash))
            {
                // Create the security claims for the signed-in user
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.Role),
                    // Add additional claims if necessary
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Sign in the user with the created claims identity
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                // Set the success message
                TempData["SuccessMessage"] = "Login successful!";

                // Redirect to the original requested URL if it's a local URL; otherwise, redirect to the homepage
                //if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                //{
                //    return LocalRedirect(ReturnUrl);
                //}
                Userinstance.email = user.Email;
                Userinstance.role = user.Role;
                Userinstance.id = user.Id;
                Userinstance.username = user.FirstName + " " + user.LastName;

                if (user.Role == ("Admin"))
                {
                    return RedirectToPage("/academic_dashboard");
                }
                else if (user.Role == ("SocietyHead"))
                {
                    return RedirectToPage("/society_dashboard");
                }
                else if (user.Role == ("Student"))
                {
                    return RedirectToPage("/student_dashboard");
                }
                return RedirectToPage("/Error");
            }
            else
            {
                // If authentication failed, show an error message
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }
        }

    }
}
