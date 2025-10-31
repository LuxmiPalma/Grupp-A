using Microsoft.AspNetCore.Identity;

namespace DAL.Entities;

public class User : IdentityUser<int>
{
    public List<Session> Bookings { get; set; }
}
