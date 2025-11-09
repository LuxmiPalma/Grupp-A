using DAL.DTOs;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Services.Interfaces;

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

        public async Task<List<SessionsDto>> SearchByCategory(string category)
        {
            var sessions = await _sessionRepository.GetAllAsync();
            var filteredSessions = sessions
                .Where(s => s.Category != null && s.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .Select(s => new SessionsDto
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
                })
                .ToList();
            return filteredSessions;
        }

        public async Task<List<SessionsDto>> GetSessionsByCategoryAsync(string category)
        {
            var sessions = await _sessionRepository.GetAllAsync();
            return sessions
                .Where(s => !string.IsNullOrEmpty(s.Category) &&
                            s.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .Select(s => new SessionsDto
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
                })
                .ToList();
        }
    }
}