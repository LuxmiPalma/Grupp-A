using DAL.DTOs;
using DAL.Entitites;
using DAL.Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;

        public SessionService(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<List<Session>> GetAllSessionsAsync()
        {
            return await _sessionRepository.GetAllAsync();
        }

        public async Task<Session?> GetSessionByIdAsync(int id)
        {
            return await _sessionRepository.GetByIdAsync(id);
        }


        public async Task<List<SessionDto>> SearchByCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                throw new ArgumentException("Category cannot be null or empty.", nameof(category));
            }

            var sessions = await _sessionRepository.GetAllAsync();

            var matchedSession = sessions.Where(s => s.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).Select(s => new SessionDto
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                Category = s.Category
            }).ToList();

            return matchedSession;




        }
    }

}
