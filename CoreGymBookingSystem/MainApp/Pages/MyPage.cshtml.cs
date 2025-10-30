using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

[Authorize]
public class MyPageModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public MyPageModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        Input = new InputModel();
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        [MinLength(6)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public async Task<IActionResult> OnGetAsync()
    {
        IdentityUser user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        Input.UserName = user.UserName;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string action)
    {
        if (string.IsNullOrWhiteSpace(action))
        {
            return await OnGetAsync();
        }

        IdentityUser user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        if (action == "change-username")
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(Input.UserName))
            {
                return Page();
            }

            string currentUserName = user.UserName;
            if (Input.UserName != currentUserName)
            {
                IdentityResult setName = await _userManager.SetUserNameAsync(user, Input.UserName);
                if (!setName.Succeeded)
                {
                    foreach (IdentityError err in setName.Errors)
                    {
                        ModelState.AddModelError(string.Empty, err.Description);
                    }
                    return Page();
                }

                await _signInManager.RefreshSignInAsync(user);
                TempData["StatusMessage"] = "Username updated.";
            }

            return RedirectToPage();
        }

        if (action == "change-password")
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (string.IsNullOrEmpty(Input.CurrentPassword) || string.IsNullOrEmpty(Input.NewPassword))
            {
                ModelState.AddModelError(string.Empty, "Please provide current and new passwords.");
                return Page();
            }

            IdentityResult result = await _userManager.ChangePasswordAsync(user, Input.CurrentPassword, Input.NewPassword);
            if (!result.Succeeded)
            {
                foreach (IdentityError err in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            TempData["StatusMessage"] = "Password updated.";
            return RedirectToPage();
        }

        return RedirectToPage();
    }
}
