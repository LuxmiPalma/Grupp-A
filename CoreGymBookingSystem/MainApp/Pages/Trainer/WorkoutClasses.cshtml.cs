using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DAL.DTOs;
using Service.Interfaces;

namespace MainApp.Pages.Trainer
{
    [Authorize(Roles = "Trainer")] 
    public class WorkoutClassesModel : PageModel
    {
        private readonly ICrudWorkoutClassService _service;

        public WorkoutClassesModel(ICrudWorkoutClassService service)
        {
            _service = service;
        }

        public IReadOnlyList<WorkoutClassDto> Classes { get; private set; } = Array.Empty<WorkoutClassDto>();

        [BindProperty]
        public CreateInput Input { get; set; } = new();

        public IEnumerable<WorkoutType> AllowedTypes => new[]
        {
            WorkoutType.Yoga,
            WorkoutType.Cardio,
            WorkoutType.StrengthTraining,
            WorkoutType.Dance
        };

        public async Task OnGet(CancellationToken ct)
        {
            Classes = await _service.GetAllAsync(onlyActive: true, ct);
        }

        public async Task<IActionResult> OnPostCreateAsync(CancellationToken ct)
        {
            if (!AllowedTypes.Contains(Input.WorkoutType))
            {
                ModelState.AddModelError(nameof(Input.WorkoutType), "Ogiltig typ vald.");
            }

            if (!ModelState.IsValid)
            {
                Classes = await _service.GetAllAsync(onlyActive: true, ct);
                return Page();
            }

            var dto = new CreateWorkoutClassDto
            {
                WorkoutType = Input.WorkoutType,
                Description = Input.Description?.Trim(),
                Duration = Input.DurationMinutes,
                DifficultyLevel = Input.DifficultyLevel
            };

            var created = await _service.CreateAsync(dto, ct);
            TempData["Toast"] = $"Workout '{created.WorkoutType}' skapad ({created.Duration} min).";

            return RedirectToPage(); 
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id, CancellationToken ct)
        {
            var ok = await _service.DeleteAsync(id, ct);
            TempData["Toast"] = ok ? "Workout raderad." : "Kunde inte radera.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostToggleAsync(string id, bool isActive, CancellationToken ct)
        {
            var ok = await _service.ToggleActiveAsync(id, isActive, ct);
            TempData["Toast"] = ok ? (isActive ? "Aktiverad." : "Inaktiverad.") : "Åtgärd misslyckades.";
            return RedirectToPage();
        }

        public class CreateInput
        {
            [Required]
            public WorkoutType WorkoutType { get; set; }

            [Range(15, 180, ErrorMessage = "Duration måste vara 15–180 min.")]
            public int DurationMinutes { get; set; } = 60;

            [Required]
            public DifficultyLevel DifficultyLevel { get; set; } = DifficultyLevel.Beginner;

            [MaxLength(500)]
            public string? Description { get; set; }
        }
    }
}
