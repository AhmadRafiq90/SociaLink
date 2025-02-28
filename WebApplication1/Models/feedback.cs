using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Feedback
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string SocietyId { get; set; }

        [Required]
        public string StudentId { get; set; } // Foreign Key

        [StringLength(1000)]
        public string Comments { get; set; } // Comments section

        [Required]
        public double AverageRating { get; set; } // Average rating out of 5

        public DateTime FeedbackDate { get; set; }

        // Navigation property
        public virtual User Student { get; set; }
    }
}
