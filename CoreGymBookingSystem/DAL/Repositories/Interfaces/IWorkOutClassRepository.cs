using DAL.DTOs;
using DAL.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IWorkOutClassRepository
    {
        Task<bool> ExistsAsync(WorkoutType type, CancellationToken ct);
        Task AddAsync(WorkoutClass entity, CancellationToken ct);
        Task<WorkoutClass?> GetByIdAsync(Guid id, CancellationToken ct);
    }
}
