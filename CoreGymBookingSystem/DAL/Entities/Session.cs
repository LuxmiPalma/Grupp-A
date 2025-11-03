using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class Session
{
    /// <summary>
    /// Unique identifier of the session.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Summary of the session.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Summary of the activities.
    /// </summary>
    public string Description { get; set; } = string.Empty;
    public string InstructorId { get; set; } = string.Empty;
    public IdentityUser? Instructor { get; set; }

    public int CurrentBookings { get; set; }
    public string DayOfWeek { get; set; } = string.Empty;

    /// <summary>
    /// The day of the week the session starts.
    /// </summary>
    [NotMapped, Obsolete( "Use StartTime.DayOfWeek instead." )]
    public string DayOfWeek => StartTime.DayOfWeek.ToString();
}
