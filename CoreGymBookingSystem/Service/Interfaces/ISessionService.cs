using DAL.Entities;

namespace Services.Interfaces
{
    public interface ISessionService
    {
        Task<List<Session>> GetAllSessionsAsync();
        Task<Session?> GetSessionByIdAsync(int id);


    }
}
