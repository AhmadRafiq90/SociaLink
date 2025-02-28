using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using WebApplication1.Models; // Ensure this uses your actual namespace
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DBcontext;
using WebApplication1.Hasher;
using Microsoft.IdentityModel.Tokens;


namespace WebApplication1.Pages
{
    public class SignupModel : PageModel
    {
        private readonly FASTSocietyManagementContext _context; // Update ApplicationDbContext with your actual DbContext

        public SignupModel(FASTSocietyManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [StringLength(256)]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [StringLength(512, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [StringLength(50)]
            public string FirstName { get; set; }

            [Required]
            [StringLength(50)]
            public string LastName { get; set; }

            [Required]
            [StringLength(50)]
            public string Role { get; set; }

            public string? Interests { get; set; }

        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            // Check if email is already taken
            var existingUser = await _context.Users
                                             .AsNoTracking()
                                             .AnyAsync(u => u.Email == Input.Email);
            if (existingUser)
            {
                ModelState.AddModelError("Input.Email", "Email already in use.");
                return Page();
            }
            if (Input.Password.IsNullOrEmpty() || Input.Password != Input.ConfirmPassword || Input.Password.Length < 8)
            {                 
                ModelState.AddModelError("Input.ConfirmPassword", "Passwords do not match or are invalid");
                return Page();
            }

            var user = new User
            {
                Id = Guid.NewGuid().ToString(), // Manually set the Id to a new GUID
                Email = Input.Email,
                PasswordHash = SecurePasswordHasher.Hash(Input.Password), // This should ideally be hashed
                FirstName = Input.FirstName,
                LastName = Input.LastName,
                Role = Input.Role,
                Interests = Input.Interests ?? "" // Handle null Interests
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Redirect after successful registration, possibly to a login page
            return RedirectToPage("/Login");
        }


    }
}
