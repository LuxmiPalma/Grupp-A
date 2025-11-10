// DAL/DTOs/SessionCreateDto.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.DTOs
{
    public class SessionCreateDto
    {
        [Required, StringLength(80)]
        public string Title { get; set; }

        [StringLength(400)]
        public string Description { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Range(1, 200)]
        public int MaxParticipants { get; set; }

        // Keep as string because your entity stores Category as string
        [Required]
        [RegularExpression("Yoga|Running|Weightloss|Cardio|Bodybuilding|Nutrition",
            ErrorMessage = "Category must be one of the allowed values.")]
        public string Category { get; set; }

        // NOTE: int, because IdentityUser<int>
        [Required]
        public int InstructorId { get; set; }
    }
}
