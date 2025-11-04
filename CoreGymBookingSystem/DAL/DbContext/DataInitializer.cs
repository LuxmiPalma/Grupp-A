using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DbContext
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public DataInitializer(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        public void SeedData()
        {
            _dbContext.Database.Migrate();
            SeedRoles();
            SeedUsers();
            SeedSessions();
        }

        // Här finns möjlighet att uppdatera dina användares loginuppgifter
        private void SeedUsers()
        {
            AddUserIfNotExists("GruppA@gmail.com", "Hejsan123#", new string[] { "Admin" });
            AddUserIfNotExists("GruppA2@gmail.com", "Hejsan123#", new string[] { "Member" });
            AddUserIfNotExists("GruppA3@gmail.com", "Hejsan123#", new string[] { "Trainer" });
        }

        // Här finns möjlighet att uppdatera dina användares roller
        private void SeedRoles()
        {
            AddRoleIfNotExisting("Admin");
            AddRoleIfNotExisting("Member");
            AddRoleIfNotExisting("Trainer");
        }

        private void AddRoleIfNotExisting(string roleName)
        {
            var role = _dbContext.Roles.FirstOrDefault(r => r.Name == roleName);
            if (role == null)
            {
                _dbContext.Roles.Add(new IdentityRole { Name = roleName, NormalizedName = roleName });
                _dbContext.SaveChanges();
            }
        }

        private void AddUserIfNotExists(string userName, string password, string[] roles)
        {
            if (_userManager.FindByEmailAsync(userName).Result != null) return;

            var user = new IdentityUser
            {
                UserName = userName,
                Email = userName,
                EmailConfirmed = true
            };
            _userManager.CreateAsync(user, password).Wait();
            _userManager.AddToRolesAsync(user, roles).Wait();
        }

        private void SeedSessions()
        {
            if (!_dbContext.Sessions.Any())
            {
                var trainer = _dbContext.Users.FirstOrDefault(u => u.Email == "GruppA3@gmail.com");
                if (trainer != null)
                {
                    _dbContext.Sessions.AddRange(
                        new DAL.Entitites.Session
                        {
                            Title = "Morning Yoga",
                            Description = "Start your day with calm yoga.",
                            Category = "Yoga",
                            InstructorId = trainer.Id,
                            StartTime = DateTime.Today.AddHours(6),
                            EndTime = DateTime.Today.AddHours(8),
                            MaxParticipants = 15,
                            CurrentBookings = 7,
                            DayOfWeek = "Monday"
                        },
                        new DAL.Entitites.Session
                        {
                            Title = "Cardio Blast",
                            Description = "High-energy cardio to burn fat fast!",
                            Category = "Cardio",
                            InstructorId = trainer.Id,
                            StartTime = DateTime.Today.AddHours(10),
                            EndTime = DateTime.Today.AddHours(12),
                            MaxParticipants = 20,
                            CurrentBookings = 12,
                            DayOfWeek = "Wednesday"
                        },
                        new DAL.Entitites.Session
                        {
                            Title = "Evening Strength",
                            Description = "Weightlifting and resistance training.",
                            Category = "Strength",
                            InstructorId = trainer.Id,
                            StartTime = DateTime.Today.AddHours(17),
                            EndTime = DateTime.Today.AddHours(19),
                            MaxParticipants = 10,
                            CurrentBookings = 5,
                            DayOfWeek = "Friday"
                        }
                    );
                    _dbContext.SaveChanges();
                }
            }
        }

    }
}


    

