using System.Threading.Tasks;
using System.Linq;
using MainApp.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace MainApp.Pages.Member
{
    
    public class CategoryModel : PageModel
    {
        public readonly ISessionService _sessionService;

        public List<SessionViewModel> SearchByCategory { get; set; } = new List<SessionViewModel>();

        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; }

        public CategoryModel(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public async Task OnGetAsync()
        {
                       await LoadSessionsAsync();
        }

        public async Task OnPostAsync()
        {
            await LoadSessionsAsync();
        }

        private async Task LoadSessionsAsync()
        {
            if (string.IsNullOrWhiteSpace(SelectedCategory))
            {
                SearchByCategory.Clear();
                return;
            }
            var sessions = await _sessionService.SearchByCategory(SelectedCategory);
            SearchByCategory = sessions.Select(s => new SessionViewModel
            {
               
                Title = s.Title,
                Description = s.Description,
                Category = s.Category
            }).ToList();
        }
        
    }
}
