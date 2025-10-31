using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace MainApp.Pages;

public class ClassDetailsModel : PageModel
{
    private readonly ISessionService _sessionService;

    public ClassDetailsModel(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    public Session? Session { get; set; }
    public List<Session> Sessions { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        if (id == null)
            return Page();

        Session = await _sessionService.GetSessionByIdAsync(id);
        Sessions = await _sessionService.GetAllSessionsAsync();

        return Page();
    }
}
