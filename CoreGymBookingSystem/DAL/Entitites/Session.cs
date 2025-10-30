

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entitites;

public class Session
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string InstructorId { get; set; } = string.Empty;
    public IdentityUser Instructor { get; set; }

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int MaxParticipants { get; set; }

    [NotMapped, Obsolete]
    public int CurrentBookings { get; set; }

    [NotMapped, Obsolete]
    public string DayOfWeek => StartTime.DayOfWeek.ToString();

}
