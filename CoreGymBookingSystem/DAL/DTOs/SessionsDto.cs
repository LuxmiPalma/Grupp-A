using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs
{
    public class SessionsDto

    {


     
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Summary of the activities.
        /// </summary>
        public string Description { get; set; } = string.Empty;


        /// <summary>
        /// Summary of the activities.
        /// </summary>
        public string Category { get; set; } = string.Empty;

        public int Id { get; set; }

        public string DayOfWeek { get; set; } = string.Empty;

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string? InstructorUserName { get; set; }

        public int MaxParticipants { get; set; }
        public int CurrentBookings { get; set; }
    }
}
