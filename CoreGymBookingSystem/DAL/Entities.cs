
namespace DAL
{
    internal class Entities
    {
        internal class Session
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string InstructorId { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public int MaxParticipants { get; set; }
            public int CurrentBookings { get; set; }
        }
    }
}