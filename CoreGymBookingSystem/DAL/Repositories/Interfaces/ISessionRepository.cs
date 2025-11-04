using DAL.Entities;

namespace DAL.Repositories.Interfaces;

public interface ISessionRepository
{
    Task<List<Session>> GetAllAsync();
    Task<Session?> GetByIdAsync(int id);
    Task AddAsync(Session session);
    Task SaveChangesAsync();
}
