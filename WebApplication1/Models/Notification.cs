using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Notification
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string UserId { get; set; } // Foreign Key

        [Required, StringLength(2048)]
        public string Message { get; set; }

        public bool IsRead { get; set; }

        public DateTime NotificationDate { get; set; }

        // Navigation property
        public virtual User User { get; set; }
    }
}
