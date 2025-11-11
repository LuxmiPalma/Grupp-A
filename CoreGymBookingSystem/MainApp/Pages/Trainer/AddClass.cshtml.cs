using System;
using System.Security.Claims;
using System.Threading.Tasks;
using DAL.DTOs;
using MainApp.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces; // ISessionService

namespace MainApp.Pages.Trainer
{
    [Authorize(Roles = "Trainer")]
    public class AddClassModel : PageModel
    {
        private readonly ISessionService _sessionService;

        [BindProperty]
        public TrainerDashboardVm Vm { get; set; }

        public AddClassModel(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public void OnGet()
        {
            Vm = new TrainerDashboardVm();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Server-side guards (cannot rely on client only)
            DateTime todayMidnight = DateTime.Today;
            if (Vm.Create.StartTime < todayMidnight || Vm.Create.EndTime < todayMidnight)
            {
                ModelState.AddModelError(string.Empty, "You cannot schedule classes in the past.");
                return Page();
            }

            if (Vm.Create.EndTime.Date != Vm.Create.StartTime.Date)
            {
                ModelState.AddModelError(string.Empty, "End time must be on the same day as the start time.");
                return Page();
            }

            if (Vm.Create.EndTime <= Vm.Create.StartTime)
            {
                ModelState.AddModelError(string.Empty, "End time must be after the start time.");
                return Page();
            }

            int instructorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            SessionCreateDto dto = new SessionCreateDto();
            dto.Title = Vm.Create.Title;
            dto.Description = Vm.Create.Description;
            dto.StartTime = Vm.Create.StartTime;
            dto.EndTime = Vm.Create.EndTime;
            dto.MaxParticipants = Vm.Create.MaxParticipants;
            dto.Category = Vm.Create.Category; // enum in your VM/DTO
            dto.InstructorId = instructorId;

            try
            {
                await _sessionService.CreateAsync(dto);
            }
            catch (InvalidOperationException ex)
            {
                // e.g., overlap: "You already have a class scheduled during this time."
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }

            TempData["Success"] = "Class added successfully!";
            return RedirectToPage("/Trainer/Dashboard");
        }
    }
}
