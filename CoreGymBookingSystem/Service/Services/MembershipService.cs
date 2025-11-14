using DAL.Entities;

namespace Service.Services;

public interface IMembershipService
{
    Task<List<MembershipType>> GetAllAsync();
    Task<MembershipType?> GetByIdAsync(int id);
}

