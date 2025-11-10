using DAL.DTOs;
using MainApp.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MainApp.Pages.Trainer
{
    [Authorize(Roles = "Trainer")]
    public class DashboardModel : PageModel
    {
        private readonly ISessionService _sessionService;

        [BindProperty]
        public TrainerDashboardVm Vm { get; set; }

        public string SuccessMessage { get; set; }

        public DateTime PrevWeekStart { get; set; }
        public DateTime NextWeekStart { get; set; }

        public DashboardModel(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public async Task<IActionResult> OnGet(DateTime? start)
        {
            Vm = new TrainerDashboardVm();

            DateTime weekStart = GetWeekStart(start ?? DateTime.Today);
            Vm.WeekStart = weekStart;

            PrevWeekStart = weekStart.AddDays(-7);
            NextWeekStart = weekStart.AddDays(7);

            int instructorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            Vm.Sessions = await _sessionService.GetForInstructorWeekAsync(instructorId, weekStart);

            if (TempData.ContainsKey("Success"))
            {
                SuccessMessage = TempData["Success"] as string;
            }

            return Page();
        }

        private DateTime GetWeekStart(DateTime anyDay)
        {
            int diff = (7 + (int)anyDay.DayOfWeek - (int)DayOfWeek.Monday) % 7;
            return anyDay.Date.AddDays(-diff);
        }
    }
}
