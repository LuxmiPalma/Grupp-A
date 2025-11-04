using DAL.DTOs;
using DAL.Entitites;

namespace Services.Interfaces
{
    public interface ISessionService
    {
        Task<List<Session>> GetAllSessionsAsync();
        Task<Session?> GetSessionByIdAsync(int id);

        Task<List<SessionDto>> SearchByCategory(string category);
    }
}
