using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }

        [Required, StringLength(256)]
        public string Email { get; set; }

        [Required, StringLength(512)]
        public string PasswordHash { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        [Required, StringLength(50)]
        public string Role { get; set; } // Could be "Student", "SocietyHead", "Admin"

        // Assuming Interests is a serialized JSON string or similar
        [StringLength(1024)]
        public string Interests { get; set; }

        public virtual ICollection<SocietyMembership> SocietyMemberships { get; set; }
        public virtual ICollection<Announcement> Announcements { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }


        // Add to your User model
        [NotMapped] // This ensures it doesn't get mapped to your database
        [DataType(DataType.Password)]
        [Compare("PasswordHash", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
