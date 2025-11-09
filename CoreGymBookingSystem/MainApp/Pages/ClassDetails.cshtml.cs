using DAL.DTOs;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Interfaces;
using Services.Interfaces;

namespace MainApp.Pages
{
    public class ClassDetailsModel : PageModel
    {
        private readonly ISessionService _sessionService;
        private readonly IBookingService _bookingService;
        private readonly UserManager<User> _userManager;

        public ClassDetailsModel(ISessionService sessionService, IBookingService bookingService, UserManager<User> userManager)
        {
            _sessionService = sessionService;
            _bookingService = bookingService;
            _userManager = userManager;
        }

        public Session? Session { get; set; }
        public List<SessionsDto> Sessions { get; set; } = new();
        public List<int> UserBookings { get; set; } = new(); // Vilka sessioner användaren har bokat
        // För att markera aktiv knapp i vyn
        public string Filter { get; set; } = "all";

        [TempData]
        public string? StatusMessage { get; set; }

        // /ClassDetails?id=5&filter=fitness
        public async Task<IActionResult> OnGetAsync(int? id, string? filter)
        {
            // Sätt valt filter (default "all")
            Filter = string.IsNullOrWhiteSpace(filter) ? "all" : filter.ToLowerInvariant();
            // id är nullable nu, så detta funkar
            if (id.HasValue)
            {
                Session = await _sessionService.GetSessionByIdAsync(id.Value);
            }
            if (Filter == "all")
            {
                // Hämta alla (entities) och mappa till DTO här
                var allSessions = await _sessionService.GetAllSessionsAsync();
                Sessions = allSessions.Select(s => new SessionsDto
                {
                    Id = s.Id,
                    Title = s.Title,
                    Description = s.Description,
                    Category = s.Category,
                    DayOfWeek = s.StartTime.DayOfWeek.ToString(), // Använd StartTime.DayOfWeek istället
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    InstructorUserName = s.Instructor?.UserName,
                    MaxParticipants = s.MaxParticipants,
                    CurrentBookings = s.Bookings.Count // Använd Bookings.Count istället
                }).ToList();
            }
            else
            {
                // Hämta redan projicerad lista från service (DTO-version)
                Sessions = await _sessionService.GetSessionsByCategoryAsync(Filter);
            }

            // Ladda medlemmens bokningar
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var bookings = await _bookingService.GetMyBookingsAsync(user.Id);
                UserBookings = bookings.Select(b => b.Id).ToList(); // Använd b.Id istället för b.SessionId
            }

            return Page();
        }

        public async Task<IActionResult> OnPostBookAsync(int sessionId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToPage("/Account/Login", new { area = "Identity" });

            var (success, message) = await _bookingService.BookSessionAsync(user.Id, sessionId);
            StatusMessage = message;

            return RedirectToPage(new { filter = Filter });
        }

        public async Task<IActionResult> OnPostCancelAsync(int sessionId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToPage("/Account/Login", new { area = "Identity" });

            var (success, message) = await _bookingService.CancelBookingAsync(user.Id, sessionId);
            StatusMessage = message;

            return RedirectToPage(new { filter = Filter });
        }
    }
}