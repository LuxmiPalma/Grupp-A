using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class Session
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public User Instructor { get; set; }

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int MaxParticipants { get; set; }
    public List<User> Bookings { get; set; }

    [NotMapped, Obsolete]
    public int CurrentBookings { get; set; }

    [NotMapped, Obsolete]
    public string DayOfWeek => StartTime.DayOfWeek.ToString();

}
