using MainApp.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace MainApp.Pages.Member
{
    public class CategoryModel : PageModel
    {
        public readonly ISessionService _sessionService;

        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; }

        public List<SessionViewModel> SearchbyCategory = new List<SessionViewModel>();
        public CategoryModel(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
       
        public async Task OnGetAsync()
        {
            await LoadSessions();

        }

        public async Task OnPostAsync ()
        {
            await LoadSessions();

        }


        private async Task LoadSessions ()
        {
            var sessions = await _sessionService.SearchByCategory(SelectedCategory);
            SearchbyCategory = sessions.Select(s => new SessionViewModel
            {
                Title = s.Title,
                Description = s.Description,
                Category = s.Category
            }).ToList();
        }
    }
}
