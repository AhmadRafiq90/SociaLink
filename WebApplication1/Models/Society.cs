using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Society
    {
        [Key]
        public string Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(256)]
        public string LogoURL { get; set; }

        [Required, EmailAddress, StringLength(256)]
        public string ContactEmail { get; set; }

        [Required]
        public int IsApproved { get; set; } = 0;

        // Navigation property for SocietyMemberships
        public virtual ICollection<SocietyMembership>? Memberships { get; set; }
        public virtual ICollection<Event>? Events { get; set; }
        public virtual ICollection<Announcement>? Announcements { get; set; }

    }
}
