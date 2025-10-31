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
        public async Task SeedData()
        {
            await _dbContext.Database.MigrateAsync();

            await SeedRoles();
            await SeedUsers();
            await SeedSessions();
        }

        // Här finns möjlighet att uppdatera dina användares loginuppgifter
        private async Task SeedUsers()
        {
            await AddUserIfNotExists("GruppA@gmail.com", "Hejsan123#", new string[] { "Admin" });
            await AddUserIfNotExists("GruppA2@gmail.com", "Hejsan123#", new string[] { "Member" });
            await AddUserIfNotExists("GruppA3@gmail.com", "Hejsan123#", new string[] { "Trainer" });
        }

        // Här finns möjlighet att uppdatera dina användares roller
        private async Task SeedRoles()
        {
            await AddRoleIfNotExisting("Admin");
            await AddRoleIfNotExisting("Member");
            await AddRoleIfNotExisting("Trainer");
        }

        private async Task AddRoleIfNotExisting(string roleName)
        {
            var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
            if (role == null)
            {
                await _dbContext.Roles.AddAsync(new IdentityRole<int> { Name = roleName, NormalizedName = roleName });
                await _dbContext.SaveChangesAsync();
            }
        }

        private async Task AddUserIfNotExists(string userName, string password, string[] roles)
        {
            if (_userManager.FindByEmailAsync(userName).Result != null) return;

            var user = new User
            {
                UserName = userName,
                Email = userName,
                EmailConfirmed = true
            };
            await _userManager.CreateAsync(user, password);
            await _userManager.AddToRolesAsync(user, roles);
        }

        private async Task SeedSessions()
        {
            if (!_dbContext.Sessions.Any())
            {
                var trainer = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == "GruppA3@gmail.com");
                if (trainer != null)
                {
                    await _dbContext.Sessions.AddRangeAsync(
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
                    await _dbContext.SaveChangesAsync();
                }
            }
        }

    }
}


    

