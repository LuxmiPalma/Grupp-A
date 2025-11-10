using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs
{
    public record CreateWorkoutClassDto
    {
        public WorkoutType WorkoutType { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
    }
}
