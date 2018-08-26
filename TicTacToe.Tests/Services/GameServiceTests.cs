using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Moq;
using TicTacToe.Common.BindingModels;
using TicTacToe.Common.Constants;
using TicTacToe.Data;
using TicTacToe.Models;
using TicTacToe.Services;
using TicTacToe.Services.Exceptions;
using TicTacToe.Services.Interfaces;
using TicTacToe.Tests.Mocks;

namespace TicTacToe.Tests.Services
{
    [TestClass]
    public class GameServiceTests
    {
        private TicTacToeDbContext context;
        private User user;
        private User user2;

        [TestInitialize]
        public void Initialize()
        {
            this.context = MockDbContext.GetContext();
            var guidId = Guid.NewGuid().ToString();
            this.user = new User() { Id = guidId, FirstName = "test", LastName = "test", Email = "abv@abv.bg", UserName = "abv@abv.bg" };
            var guidId2 = Guid.NewGuid().ToString();
            this.user2 = new User() { Id = guidId2, FirstName = "test2", LastName = "test2", Email = "2abv@abv.bg", UserName = "2abv@abv.bg" };
            context.Users.Add(user);
            context.Users.Add(user2);
            context.SaveChanges();
        }

        [TestMethod]
        public void CreateGame_WithValidData_IsSuccessful()
        {
            var gameValidator = new Mock<IGameResultValidator>();
            var gameService = new GameService(context, gameValidator.Object);

            gameService.Create(new CreateGameBindingModel(), user.Id);

            Assert.IsNotNull(context.Games);
            Assert.AreEqual(1, context.Games.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException),ErrorMessagesConstants.USERID_IS_NULL)]
        public void CreateGame_WithEmptyString_ReturnsException()
        {
            var gameValidator = new Mock<IGameResultValidator>();
            var gameService = new GameService(context, gameValidator.Object);

            gameService.Create(new CreateGameBindingModel(), null);
        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void GetAvailableGames_OpponentHasACreatedGameAndStateIsWaiting_ReturnsGame()
        {
            var opponentGame = new Game(){CreationDate = DateTime.Now, Name= "game", Visibility = VisibilityType.Public, State = GameState.WaitingForASecondPlayer, CreatorUserId = user2.Id, HashedPassword = "", };
            context.Games.Add(opponentGame);
            context.SaveChanges();
            var gameValidator = new Mock<IGameResultValidator>();
            var gameService = new GameService(context, gameValidator.Object);

            var games = gameService.GetAvailableGames(user.Id);
            
            Assert.AreEqual(1, games.Count);
            Assert.AreEqual(opponentGame.GameId, games.FirstOrDefault().Id);
            Assert.IsNotNull(games);
        }

        public void GetAvailableGames_OpponentHasACreatedGameAndStateIsWon_ReturnsGame()
        {
            var opponentGame = new Game() { CreationDate = DateTime.Now, Name = "game", Visibility = VisibilityType.Public, State = GameState.CreatorVictory, CreatorUserId = user2.Id, HashedPassword = "", };
            context.Games.Add(opponentGame);
            context.SaveChanges();
            var gameValidator = new Mock<IGameResultValidator>();
            var gameService = new GameService(context, gameValidator.Object);

            var games = gameService.GetAvailableGames(user.Id);

            Assert.AreEqual(0, games.Count);
            Assert.IsFalse(games.Any());
        }
    }
}
