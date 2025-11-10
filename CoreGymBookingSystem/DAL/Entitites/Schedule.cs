using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entitites
{
    public class Schedule
    {
        public int ScheduledId { get; set; }
        public DateTime WeekendDate { get; set; }
        public DateTime WeekStartDate { get; set; }

        public string CreatedBy { get; set; } = string.Empty;   

    }
}
