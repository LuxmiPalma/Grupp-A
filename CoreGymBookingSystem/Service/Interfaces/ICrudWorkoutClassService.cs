using DAL.DTOs;
using DAL.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ICrudWorkoutClassService
    {
        Task<WorkoutClassDto> CreateAsync(CreateWorkoutClassDto dto, CancellationToken ct = default);
        Task<WorkoutClassDto?> GetByIdAsync(string id, CancellationToken ct = default);
        Task<IReadOnlyList<WorkoutClassDto>> GetAllAsync(bool onlyActive = true, CancellationToken ct = default);
        Task<WorkoutClassDto?> UpdateAsync(string id, CreateWorkoutClassDto dto, CancellationToken ct = default);
        Task<bool> DeleteAsync(string id, CancellationToken ct = default);      
        Task<bool> ToggleActiveAsync(string id, bool isActive, CancellationToken ct = default);
    }
}
