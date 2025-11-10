using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs
{
    public record WorkoutClassDto
    {
        public string Id { get; set; }
        public WorkoutType WorkoutType { get; set; }

        public string Description { get; set; }

        public string Duration { get; set; }

        public DifficultyLevel DifficultyLevel { get; set; }

        public bool IsActive { get; set; }
    }

    public enum WorkoutType
    {
        Yoga,
        Cardio,
        StrengthTraining,
        Dance,
    }
    public enum DifficultyLevel
    {
        Beginner,
        Intermediate,
        Advanced
    }
}
