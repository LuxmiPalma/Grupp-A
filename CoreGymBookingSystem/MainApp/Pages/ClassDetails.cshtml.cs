using DAL.DTOs;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Interfaces;
using Services.Interfaces;

namespace MainApp.Pages;

public class ClassDetailsModel : PageModel
{
    private readonly ISessionService _sessionService;
    private readonly IBookingService _bookingService;
    private readonly UserManager<User> _userManager;

    public ClassDetailsModel(
        ISessionService sessionService,
        IBookingService bookingService,
        UserManager<User> userManager)
    {
        _sessionService = sessionService;
        _bookingService = bookingService;
        _userManager = userManager;
    }

    public Session? Session { get; set; }
    public List<SessionsDto> Sessions { get; set; } = new();
    public List<int> UserBookings { get; set; } = new();
    public string Filter { get; set; } = "all";
    [TempData] public string? StatusMessage { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id, string? filter)
    {
        Filter = string.IsNullOrWhiteSpace(filter) ? "all" : filter.ToLowerInvariant();

        if (id.HasValue)
        {
            Session = await _sessionService.GetSessionByIdAsync(id.Value);
        }

        var allSessions = await _sessionService.GetAllSessionsAsync();

        if (Filter == "all")
        {
            Sessions = allSessions.Select(s => new SessionsDto
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                Category = s.Category,
                DayOfWeek = s.StartTime.DayOfWeek.ToString(),
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                InstructorUserName = s.Instructor?.UserName,
                MaxParticipants = s.MaxParticipants,
                CurrentBookings = s.Bookings.Count
            }).ToList();
        }
        else
        {
            Sessions = await _sessionService.GetSessionsByCategoryAsync(Filter);
        }

        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            var bookings = await _bookingService.GetMyBookingsAsync(user.Id);
            UserBookings = bookings.Select(b => b.Id).ToList();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostBookAsync(int sessionId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return RedirectToPage("/Account/Login", new { area = "Identity" });

        var (success, message) = await _bookingService.BookSessionAsync(user.Id, sessionId);
        StatusMessage = message;
        return RedirectToPage(new { filter = Filter });
    }

    public async Task<IActionResult> OnPostCancelAsync(int sessionId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return RedirectToPage("/Account/Login", new { area = "Identity" });

        var (success, message) = await _bookingService.CancelBookingAsync(user.Id, sessionId);
        StatusMessage = message;
        return RedirectToPage(new { filter = Filter });
    }
}