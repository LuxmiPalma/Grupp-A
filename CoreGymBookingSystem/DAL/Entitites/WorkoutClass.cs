using DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entitites
{
    public class WorkoutClass
    {
        public Guid ClassId { get; set; }

        public WorkoutType WorkoutType { get; set; } 
        public string Description { get; set; } = string.Empty;

        public int Duration { get; set; } 

        public DifficultyLevel DifficultyLevel { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
