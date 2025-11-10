using DAL.DTOs;
using MainApp.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Interfaces;
using Services.Interfaces;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

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

            int instructorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var dto = new SessionCreateDto();
            dto.Title = Vm.Create.Title;
            dto.Description = Vm.Create.Description;
            dto.StartTime = Vm.Create.StartTime;
            dto.EndTime = Vm.Create.EndTime;
            dto.MaxParticipants = Vm.Create.MaxParticipants;
            dto.Category = Vm.Create.Category.ToString();
            dto.InstructorId = instructorId;

            await _sessionService.CreateAsync(dto);

            TempData["Success"] = "Class added successfully!";
            return RedirectToPage("/Trainer/Dashboard");
        }
    }
}
