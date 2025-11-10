using DAL.DTOs;
using DAL.Entities;
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
        private static readonly HashSet<string> Allowed =
      new HashSet<string>(StringComparer.OrdinalIgnoreCase)
      {
            "Yoga","Running","Weightloss","Cardio","Bodybuilding","Nutrition"
      };

        private readonly ISessionRepository _sessionRepository;

        public SessionService(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }





        public async Task CreateAsync(SessionCreateDto dto)
        {
            if (dto.EndTime <= dto.StartTime) throw new ArgumentException("End time must be after start time.");
            if (!Allowed.Contains(dto.Category)) throw new ArgumentException("Invalid category.");

            // Attach instructor (no extra query)
            _sessionRepository.AttachUserById(dto.InstructorId);

            var entity = new Session();
            entity.Title = dto.Title;
            entity.Description = dto.Description;
            entity.Category = dto.Category;
            entity.MaxParticipants = dto.MaxParticipants;
            entity.StartTime = dto.StartTime;
            entity.EndTime = dto.EndTime;
            entity.Instructor = new User { Id = dto.InstructorId };

            await _sessionRepository.AddAsync(entity);
            await _sessionRepository.SaveChangesAsync();
        }

        public async Task<IList<SessionListItemDto>> GetForInstructorWeekAsync(int instructorId, DateTime weekStart)
        {
            DateTime weekEnd = weekStart.Date.AddDays(7);
            var sessions = await _sessionRepository.GetByInstructorAsync(instructorId, weekStart, weekEnd);

            var list = new List<SessionListItemDto>();
            foreach (var s in sessions)
            {
                var item = new SessionListItemDto();
                item.Id = s.Id;
                item.Title = s.Title;

                // Convert string -> enum
                DAL.Enums.Category categoryEnum;
                if (!Enum.TryParse<DAL.Enums.Category>(s.Category, true, out categoryEnum))
                {
                    // Optional: handle unexpected strings
                    categoryEnum = DAL.Enums.Category.Yoga;
                }
                item.Category = categoryEnum;

                item.StartTime = s.StartTime;
                item.EndTime = s.EndTime;
                item.MaxParticipants = s.MaxParticipants;
                list.Add(item);
            }

            return list;
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
            var sessions = await  _sessionRepository.GetAllAsync();
            var filteredSessions = sessions
                .Where(s => s.Category != null && s.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .Select(s => new SessionsDto
                {
                 
                    Title = s.Title,
                    Description = s.Description,
                    Category = s.Category,
                   
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
                    DayOfWeek = s.DayOfWeek,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    InstructorUserName = s.Instructor?.UserName,
                    MaxParticipants = s.MaxParticipants,
                    CurrentBookings = s.CurrentBookings
                })
                .ToList();
        }


    }

}
