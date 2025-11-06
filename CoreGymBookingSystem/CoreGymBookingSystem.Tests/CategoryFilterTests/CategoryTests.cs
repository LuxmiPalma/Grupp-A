using Service.Services;
using Services.Interfaces;
using Moq;

using DAL.Repositories.Interfaces;

namespace CoreGymBookingSystem.Tests
{
    [TestClass]
    public class CategoryTests
    {
        private ISessionService _sut;

      

        [TestMethod]
        public async Task Check_if_All_Sessions_Belong_to_Selected_Category()
        {
           
            var mockSessionRepository = new Mock<ISessionRepository>();
            mockSessionRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<DAL.Entities.Session>
            {
                new DAL.Entities.Session { Title = "Evening Strength Training", Description = "Build your strength.", Category = "Strength" },
      
                new DAL.Entities.Session { Title = "Strength and Conditioning", Description = "Improve your overall strength.", Category = "Strength" }
            });
            _sut = new SessionService(mockSessionRepository.Object);
            string selectedCategory = "Strength";
           
            var result = await _sut.SearchByCategory(selectedCategory);
      
            Assert.IsTrue(result.All(s => s.Category.Equals(selectedCategory, StringComparison.OrdinalIgnoreCase)));



        }

        [TestMethod]
        public async Task Check_Searches_With_No_Matches_Returns_Message()
        {
           
            var mockSessionRepository = new Mock<ISessionRepository>();
            mockSessionRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<DAL.Entities.Session>
            {
                new DAL.Entities.Session { Title = "Morning Yoga", Description = "Start your day with calm yoga.", Category = "Yoga" },
                new DAL.Entities.Session { Title = "Cardio Blast", Description = "High-energy cardio to burn fat fast!", Category = "Cardio" }
            });
            _sut = new SessionService(mockSessionRepository.Object);
            string selectedCategory = "Strength";
          
            var result = await _sut.SearchByCategory(selectedCategory);
         
            Assert.IsTrue(result.Count == 0, "No sessions found for the selected category.");
        }

        [TestMethod]
        public async Task Check_If_Text_Search_Finds_Correct_Sessions()
        {
           
            var mockSessionRepository = new Mock<ISessionRepository>();
            mockSessionRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<DAL.Entities.Session>
            {
                new DAL.Entities.Session { Title = "Morning Yoga", Description = "Start your day with calm yoga.", Category = "Yoga" },
                new DAL.Entities.Session { Title = "Cardio Blast", Description = "High-energy cardio to burn fat fast!", Category = "Cardio" },
                new DAL.Entities.Session { Title = "Evening Strength Training", Description = "Build your strength.", Category = "Strength" }
            });
            _sut = new SessionService(mockSessionRepository.Object);
            string selectedCategory = "Cardio";
          
            var result = await _sut.SearchByCategory(selectedCategory);
         
            Assert.IsTrue(result.Count == 1 && result[0].Title == "Cardio Blast");
        }


        [TestMethod]
        public async Task Check_If_Nothing_Breaks_If_No_Category_Is_Selected()
        {
            var mockSessionRepository = new Mock<ISessionRepository>();
            mockSessionRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<DAL.Entities.Session>
            {
                new DAL.Entities.Session { Title = "Morning Yoga", Description = "Start your day with calm yoga.", Category = "Yoga" },
                new DAL.Entities.Session { Title = "Cardio Blast", Description = "High-energy cardio to burn fat fast!", Category = "Cardio" },
                new DAL.Entities.Session { Title = "Evening Strength Training", Description = "Build your strength.", Category = "Strength" }
            });
            _sut = new SessionService(mockSessionRepository.Object);
            string selectedCategory = string.Empty;
          
            var result = await _sut.SearchByCategory(selectedCategory);
         
            Assert.IsTrue(result.Count == 0, "No category selected should return no results.");

        }
    }
}

