using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Announcement
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string PostedByUserId { get; set; } // Foreign Key

        public string SocietyId { get; set; } // Foreign Key, nullable

        [Required, StringLength(256)]
        public string Title { get; set; }

        [Required, StringLength(2048)]
        public string Content { get; set; }

        public DateTime PostDate { get; set; }

        // Navigation properties
        public virtual User PostedByUser { get; set; }
        public virtual Society Society { get; set; }


    }
}
