using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TicTacToe.Data;
using TicTacToe.Models;
using TicTacToe.Services;
using TicTacToe.Services.Interfaces;
using TicTacToe.Tests.Mocks;

namespace TicTacToe.Tests.Services
{
    [TestClass]
    public class UserServiceTests
    {
        private TicTacToeDbContext context;
        private IScoreService scoreService;
        private IHistoryService historyService; 
        private IUserService userService; 

        [TestInitialize]
        public void Initialize()
        {
            this.context = MockDbContext.GetContext();
            this.scoreService = new Mock<IScoreService>().Object;
            this.historyService = new Mock<IHistoryService>().Object;
            this.userService = new UserService(context, scoreService, historyService);
        }

        [TestMethod]
        public void GetUser_WithRightId_ReturnUser()
        {
            /// assert
            var guidId = new Guid().ToString();
            var user = new User() { Id = guidId, FirstName = "Name", LastName = "name2", Email = "abv@abv.bg", UserName = "abv@abv.bg"};
            context.Users.Add(user);
            context.SaveChanges();

            /// act
            var resutUser = this.userService.GetUserDetails(guidId);

            /// assert
            Assert.IsNotNull(resutUser);
            Assert.AreEqual(1, context.Users.Count());
            Assert.AreEqual(guidId, resutUser.Id);
            Assert.AreEqual(user.FirstName, resutUser.FirstName);
            Assert.AreEqual(user.LastName, resutUser.LastName);
        }

        [TestMethod]
        public void GetUser_WithEmptyString_ReturnsNull()
        {
            // act
            var resultUser = this.userService.GetUser(string.Empty);

            Assert.IsNull(resultUser);
        }

        [TestMethod]
        public void GetAllUsers_WithUsersInDb_ReturnsAllUsers()
        {
            var guidId = Guid.NewGuid().ToString();
            var guidId2 = Guid.NewGuid().ToString();
            var user = new User() { Id = guidId, FirstName = "test", LastName = "test", Email = "abv@abv.bg", UserName = "abv@abv.bg", RegistrationDate = DateTime.Now, LockoutEnd = DateTime.Now.AddDays(-2)};
            var user2 = new User() { Id = guidId2, FirstName = "test2", LastName = "test2", Email = "2abv@abv.bg", UserName = "2abv@abv.bg", RegistrationDate = DateTime.Now.AddDays(1), LockoutEnd = DateTime.Now.AddDays(2) };
            context.Add(user);
            context.Add(user2);
            context.SaveChanges();

            var result = this.userService.GetAllUsers();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetAllUsers_WithNoUsersInDb_ReturnsNull()
        {
            var result = this.userService.GetAllUsers();
            
            Assert.IsNotNull(result);
        }
    }
}
