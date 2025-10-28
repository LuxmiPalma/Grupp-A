using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class AdminUsersModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminUsersModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public class UserRow
    {
        public string Id { get; set; } = "";
        public string Email { get; set; } = "";
        public bool EmailConfirmed { get; set; }
        public IList<string> Roles { get; set; } = new List<string>();
    }

    public List<UserRow> AllUsers { get; set; } = new();
    public List<UserRow> Admins { get; set; } = new();
    public List<UserRow> Trainers { get; set; } = new();
    public List<UserRow> Members { get; set; } = new();

    public async Task OnGet()
    {
        var users = await _userManager.Users.AsNoTracking().ToListAsync();

        await EnsureRolesExist(new[] { "Admin", "Trainer", "Member" });

        foreach (var u in users)
        {
            var roles = await _userManager.GetRolesAsync(u);
            var row = new UserRow
            {
                Id = u.Id,
                Email = u.Email ?? u.UserName ?? "",
                EmailConfirmed = u.EmailConfirmed,
                Roles = roles
            };
            AllUsers.Add(row);
        }

        Admins = AllUsers.Where(x => x.Roles.Contains("Admin")).OrderBy(x => x.Email).ToList();
        Trainers = AllUsers.Where(x => x.Roles.Contains("Trainer")).OrderBy(x => x.Email).ToList();
        Members = AllUsers.Where(x => x.Roles.Contains("Member")).OrderBy(x => x.Email).ToList();
    }

    private async Task EnsureRolesExist(IEnumerable<string> roles)
    {
        foreach (var r in roles)
        {
            if (!await _roleManager.RoleExistsAsync(r))
            {
                await _roleManager.CreateAsync(new IdentityRole(r));
            }
        }
    }
}
