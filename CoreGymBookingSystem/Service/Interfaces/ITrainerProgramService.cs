using DAL.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ITrainerProgramService
    {
       Task<Schedule> WeeklyProgram(Schedule schedule);
        Task<List<Schedule>> GetLisOfPrograms(string Details);
        Task<Schedule> PublishProgramAsync(Schedule schedule);
        Task<Schedule> DeleteProgramAsync(int id);
    }
}
