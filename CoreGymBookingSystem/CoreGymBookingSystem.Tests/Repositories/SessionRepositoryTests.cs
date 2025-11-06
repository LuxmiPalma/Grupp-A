using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL.Entities;
using DAL.DbContext;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace CoreGymBookingSystem.Tests.Repositories
{
    [TestClass]
    public class SessionRepositoryTests
    {
        private ApplicationDbContext _context;
        private SessionRepository _repository;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            _context = new ApplicationDbContext(options);

            var trainer = new User
            {
                Id = 1,
                UserName = "trainer1",
                Email = "trainer1@gym.com",
                EmailConfirmed = true
            };

            _context.Users.Add(trainer);

            _context.Sessions.AddRange(new List<Session>
            {
                new Session
                {
                    Id = 1,
                    Title = "Morning Yoga",
                    Description = "Start your day with calm yoga.",
                    Instructor = trainer,
                    StartTime = DateTime.Today.AddHours(6),
                    EndTime = DateTime.Today.AddHours(8),
                    MaxParticipants = 15
                },
                new Session
                {
                    Id = 2,
                    Title = "Evening Strength",
                    Description = "Weightlifting and resistance training.",
                    Instructor = trainer,
                    StartTime = DateTime.Today.AddHours(17),
                    EndTime = DateTime.Today.AddHours(19),
                    MaxParticipants = 10
                }
            });
            _context.SaveChanges();

            _repository = new SessionRepository(_context);
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnAllSessions()
        {
            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Morning Yoga", result[0].Title);
        }

        [TestMethod]
        public async Task GetByIdAsync_ShouldReturnCorrectSession_WhenIdExists()
        {
            // Act
            var result = await _repository.GetByIdAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Morning Yoga", result.Title);
            Assert.AreEqual("trainer1@gym.com", result.Instructor.Email);
        }

        [TestMethod]
        public async Task GetByIdAsync_ShouldReturnNull_WhenIdNotFound()
        {
            // Act
            var result = await _repository.GetByIdAsync(999);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task AddAsync_ShouldAddNewSession()
        {
            // Arrange
            var trainer = _context.Users.First();
            var newSession = new Session
            {
                Id = 3,
                Title = "Cardio Blast",
                Description = "High-energy cardio to burn fat fast!",
                Instructor = trainer,
                StartTime = DateTime.Today.AddHours(10),
                EndTime = DateTime.Today.AddHours(12),
                MaxParticipants = 20
            };

            // Act
            await _repository.AddAsync(newSession);
            await _repository.SaveChangesAsync();

            // Assert
            var result = await _repository.GetAllAsync();
            Assert.AreEqual(3, result.Count);
            Assert.IsTrue(result.Any(s => s.Title == "Cardio Blast"));
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
