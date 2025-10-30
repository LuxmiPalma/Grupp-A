using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Admin")]
public class AdminDashboardModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;

    public AdminDashboardModel(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public record UserRow(string Email, bool EmailConfirmed, IList<string> Roles);

    public int TotalUsers { get; set; }
    public int MemberCount { get; set; }
    public int CoachCount { get; set; }

    public List<UserRow> Members { get; set; } = new();
    public List<UserRow> Coaches { get; set; } = new();
    public List<UserRow> Admins { get; set; } = new();

    public async Task OnGet()
    {
        var users = await _userManager.Users.AsNoTracking().ToListAsync();
        TotalUsers = users.Count;

        foreach (var u in users)
        {
            var roles = await _userManager.GetRolesAsync(u);
            var row = new UserRow(u.Email ?? u.UserName ?? "", u.EmailConfirmed, roles);

            if (roles.Contains("Member")) Members.Add(row);
            if (roles.Contains("Coach") || roles.Contains("Trainer")) Coaches.Add(row);
            if (roles.Contains("Admin")) Admins.Add(row);
        }

        MemberCount = Members.Count;
        CoachCount = Coaches.Count;
    }
}
