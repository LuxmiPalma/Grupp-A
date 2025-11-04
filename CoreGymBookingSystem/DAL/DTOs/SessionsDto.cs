using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs
{
    public class SessionsDto

    {


     
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Summary of the activities.
        /// </summary>
        public string Description { get; set; } = string.Empty;


        /// <summary>
        /// Summary of the activities.
        /// </summary>
        public string Category { get; set; } = string.Empty;
    }
}
