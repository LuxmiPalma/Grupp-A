using DAL.DTOs;
using DAL.Entitites;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class WorkoutService : ICrudWorkoutClassService
    {
        private readonly List<WorkoutClass> _store = new();
        public WorkoutService()
        {
            _store.AddRange(new[]
        {
            new WorkoutClass { ClassId = Guid.NewGuid(), WorkoutType = WorkoutType.Yoga, Description = "Calm flow", Duration = 60, DifficultyLevel = DifficultyLevel.Beginner },
            new WorkoutClass { ClassId = Guid.NewGuid(), WorkoutType = WorkoutType.Cardio, Description = "Intervals",  Duration = 45, DifficultyLevel = DifficultyLevel.Intermediate },
            new WorkoutClass { ClassId = Guid.NewGuid(), WorkoutType = WorkoutType.StrengthTraining, Description = "Fat burn mix",  Duration = 50, DifficultyLevel = DifficultyLevel.Intermediate },
            new WorkoutClass { ClassId = Guid.NewGuid(), WorkoutType = WorkoutType.Dance, Description = "HIIT session",  Duration = 30, DifficultyLevel = DifficultyLevel.Advanced },
        });
        }
        public Task<WorkoutClassDto> CreateAsync(CreateWorkoutClassDto dto, CancellationToken ct = default)
        {
            Validate(dto);

            var entity = new WorkoutClass
            {
                ClassId = Guid.NewGuid(),
                WorkoutType = dto.WorkoutType,
                Description = dto.Description,
                Duration = dto.Duration,
                DifficultyLevel = dto.DifficultyLevel,
                IsActive = true
            };

            _store.Add(entity);
            return Task.FromResult(ToDto(entity));
        }

        public Task<WorkoutClassDto?> GetByIdAsync(string id, CancellationToken ct = default)
        {
            if (!Guid.TryParse(id, out var gid)) return Task.FromResult<WorkoutClassDto?>(null);
            var found = _store.FirstOrDefault(x => x.ClassId == gid);
            return Task.FromResult(found is null ? null : ToDto(found));
        }

        public Task<IReadOnlyList<WorkoutClassDto>> GetAllAsync(bool onlyActive = true, CancellationToken ct = default)
        {
            var q = _store.AsEnumerable();
            if (onlyActive) q = q.Where(x => x.IsActive);
            var list = q.Select(ToDto).ToList().AsReadOnly();
            return Task.FromResult((IReadOnlyList<WorkoutClassDto>)list);
        }

        public Task<WorkoutClassDto?> UpdateAsync(string id, CreateWorkoutClassDto dto, CancellationToken ct = default)
        {
            Validate(dto);

            if (!Guid.TryParse(id, out var gid)) return Task.FromResult<WorkoutClassDto?>(null);
            var entity = _store.FirstOrDefault(x => x.ClassId == gid);
            if (entity is null) return Task.FromResult<WorkoutClassDto?>(null);

            entity.WorkoutType = dto.WorkoutType;
            entity.Description = dto.Description;
            entity.Duration = dto.Duration;
            entity.DifficultyLevel = dto.DifficultyLevel;
            

            return Task.FromResult<WorkoutClassDto?>(ToDto(entity));
        }

        public Task<bool> DeleteAsync(string id, CancellationToken ct = default)
        {
            if (!Guid.TryParse(id, out var gid)) return Task.FromResult(false);
            var entity = _store.FirstOrDefault(x => x.ClassId == gid);
            if (entity is null) return Task.FromResult(false);
            _store.Remove(entity);
            return Task.FromResult(true);
        }

        public Task<bool> ToggleActiveAsync(string id, bool isActive, CancellationToken ct = default)
        {
            if (!Guid.TryParse(id, out var gid)) return Task.FromResult(false);
            var entity = _store.FirstOrDefault(x => x.ClassId == gid);
            if (entity is null) return Task.FromResult(false);
            entity.IsActive = isActive;
            return Task.FromResult(true);
        }

        private static void Validate(CreateWorkoutClassDto dto)
        {
            if (dto.Duration < 15 || dto.Duration > 180)
                throw new ArgumentOutOfRangeException(nameof(dto.Duration), "Duration måste vara mellan 15 och 180 minuter.");
        }

        private static WorkoutClassDto ToDto(WorkoutClass e) =>
         new WorkoutClassDto
         {
             Id = e.ClassId.ToString(),
             WorkoutType = e.WorkoutType,
             Description = e.Description,
             Duration = e.Duration.ToString(),
             DifficultyLevel = e.DifficultyLevel,
             IsActive = e.IsActive
         };
    }
}
