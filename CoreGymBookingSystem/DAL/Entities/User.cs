using Microsoft.AspNetCore.Identity;

namespace DAL.Entities;


/// <summary>
/// A gym account.
/// </summary>
public class User : IdentityUser<int>
{
    /// <summary>
    /// All bookings made by this user.
    /// </summary>
    public List<Session> Bookings { get; set; } = [];
}
