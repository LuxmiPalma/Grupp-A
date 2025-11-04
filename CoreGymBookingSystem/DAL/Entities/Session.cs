

using Microsoft.AspNetCore.Identity;

namespace DAL.Entitites;

public class Session
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string InstructorId { get; set; } = string.Empty;
    public IdentityUser? Instructor { get; set; }

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int MaxParticipants { get; set; }
    public int CurrentBookings { get; set; }
    public string DayOfWeek { get; set; } = string.Empty;

}
