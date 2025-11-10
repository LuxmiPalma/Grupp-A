using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.DTOs;
using DAL.Enums;

namespace MainApp.ViewModel
{
    public class TrainerDashboardVm
    {
        public IList<SessionListItemDto> Sessions { get; set; }
        public CreateSessionVm Create { get; set; }
        public DateTime WeekStart { get; set; }

        public TrainerDashboardVm()
        {
            Sessions = new List<SessionListItemDto>();
            Create = new CreateSessionVm();
        }
    }

    public class CreateSessionVm
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Range(1, 200)]
        public int MaxParticipants { get; set; }

        [Required]
        public Category Category { get; set; }
    }
}
