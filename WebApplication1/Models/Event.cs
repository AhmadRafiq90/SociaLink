using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string? SocietyId { get; set; }

        [StringLength(100)]
        public string societyName { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [StringLength(256)]
        public string Location { get; set; }

        public int Capacity { get; set; }

        public int IsApproved { get; set; } = 0;

        public virtual Society society { get; set; }

    }
}
