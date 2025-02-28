using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class SocietyMembership
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string UserId { get; set; } // Foreign Key

        [Required]
        public string SocietyId { get; set; } // Foreign Key

        [Required, StringLength(50)]
        public string Role { get; set; } // e.g., Member, Officer

        public DateTime JoinDate { get; set; }

        public int isApproved { get; set; } = 0;

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Society Society { get; set; }
    }
}
