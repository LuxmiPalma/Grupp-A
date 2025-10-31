using Microsoft.AspNetCore.Identity;
﻿using DAL.Entities;
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
        private readonly UserManager<User> _userManager;

        public DataInitializer(ApplicationDbContext dbContext, UserManager<User> userManager)
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
                _dbContext.Roles.Add(new IdentityRole<int> { Name = roleName, NormalizedName = roleName });
                _dbContext.SaveChanges();
            }
        }

        private void AddUserIfNotExists(string userName, string password, string[] roles)
        {
            if (_userManager.FindByEmailAsync(userName).Result != null) return;

            var user = new User
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
                        new DAL.Entities.Session
                        {
                            Title = "Morning Yoga",
                            Description = "Start your day with calm yoga.",
                            Instructor = trainer,
                            StartTime = DateTime.Today.AddHours(6),
                            EndTime = DateTime.Today.AddHours(8),
                            MaxParticipants = 15,
                        },
                        new DAL.Entities.Session
                        {
                            Title = "Cardio Blast",
                            Description = "High-energy cardio to burn fat fast!",
                            Instructor = trainer,
                            StartTime = DateTime.Today.AddHours(10),
                            EndTime = DateTime.Today.AddHours(12),
                            MaxParticipants = 20,
                        },
                        new DAL.Entities.Session
                        {
                            Title = "Evening Strength",
                            Description = "Weightlifting and resistance training.",
                            Instructor = trainer,
                            StartTime = DateTime.Today.AddHours(17),
                            EndTime = DateTime.Today.AddHours(19),
                            MaxParticipants = 10,
                        }
                    );
                    _dbContext.SaveChanges();
                }
            }
        }

    }
}


    

