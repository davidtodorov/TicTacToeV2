using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToe.Common.Enums;
using TicTacToe.Services;
using TicTacToe.Services.Interfaces;

namespace TicTacToe.Tests.Services
{
    [TestClass]
    public class GameValidator
    {
        private readonly IGameResultValidator gameResultValidator = new GameResultValidator();

        /// <summary>
        /// X X X
        /// - O -
        /// O - O
        /// </summary>
        [TestMethod]
        public void GetGameResult_WhenPlayerXWinsOnFirstRow_ReturnsWonByX()
        {
            // Arrange
            var board = "XXX-O-O-O";

            // Act
            var gameResult = this.gameResultValidator.GetGameResult(board);

            // Assert
            Assert.AreEqual(GameResult.WonByX, gameResult);
        }

        /// <summary>
        /// O - -
        /// X X X
        /// O - O
        /// </summary>
        [TestMethod]
        public void GetGameResult_WhenPlayerXWinsOnSecondRow_ReturnsWonByX()
        {
            // Arrange
            var board = "O--XXXO-O";

            // Act
            var gameResult = this.gameResultValidator.GetGameResult(board);

            // Assert
            Assert.AreEqual(GameResult.WonByX, gameResult);
        }

        /// <summary>
        /// O - -
        /// O O -
        /// X X X
        /// </summary>
        [TestMethod]
        public void GetGameResult_WhenPlayerXWinsOnThirdRow_ReturnsWonByX()
        {
            // Arrange
            var board = "O--OO-XXX";

            // Act
            var gameResult = this.gameResultValidator.GetGameResult(board);

            // Assert
            Assert.AreEqual(GameResult.WonByX, gameResult);
        }

        /// <summary>
        /// X O -
        /// X - -
        /// X O O
        /// </summary>
        [TestMethod]
        public void GetGameResult_WhenPlayerXWinsOnFirstColumn_ReturnsWonByX()
        {
            // Arrange
            var board = "XO-X--XOO";

            // Act
            var gameResult = this.gameResultValidator.GetGameResult(board);

            // Assert
            Assert.AreEqual(GameResult.WonByX, gameResult);
        }

        /// <summary>
        /// O X -
        /// O X -
        /// - X O
        /// </summary>
        [TestMethod]
        public void GetGameResult_WhenPlayerXWinsOnSecondColumn_ReturnsWonByX()
        {
            // Arrange
            var board = "OX-OX--XO";

            // Act
            var gameResult = this.gameResultValidator.GetGameResult(board);

            // Assert
            Assert.AreEqual(GameResult.WonByX, gameResult);
        }

        /// <summary>
        /// O - X
        /// - O X
        /// - O X
        /// </summary>
        [TestMethod]
        public void GetGameResult_WhenPlayerXWinsOnThirdColumn_ReturnsWonByX()
        {
            // Arrange
            var board = "O-X-OX-OX";

            // Act
            var gameResult = this.gameResultValidator.GetGameResult(board);

            // Assert
            Assert.AreEqual(GameResult.WonByX, gameResult);
        }

        /// <summary>
        /// O - X
        /// - X -
        /// X O O
        /// </summary>
        [TestMethod]
        public void GetGameResult_WhenPlayerXWinsByDiagonal_ReturnsWonByX()
        {
            // Arrange
            var board = "O-X-X-XOO";

            // Act
            var gameResult = this.gameResultValidator.GetGameResult(board);

            // Assert
            Assert.AreEqual(GameResult.WonByX, gameResult);
        }

        /// <summary>
        /// X - O
        /// - X -
        /// O O X
        /// </summary>
        [TestMethod]
        public void GetGameResult_WhenPlayerXWinsByDiagonal2_ReturnsWonByX()
        {
            // Arrange
            var board = "X-O-X-OOX";

            // Act
            var gameResult = this.gameResultValidator.GetGameResult(board);

            // Assert
            Assert.AreEqual(GameResult.WonByX, gameResult);
        }

        /// <summary>
        /// O O O
        /// X - X
        /// X - -
        /// </summary>
        [TestMethod]
        public void GetGameResult_WhenPlayerOWinsOnFirstRow_ReturnsWonByO()
        {
            // Arrange
            var board = "OOO-X-XX--";

            // Act
            var gameResult = this.gameResultValidator.GetGameResult(board);

            // Assert
            Assert.AreEqual(GameResult.WonByO, gameResult);
        }

        /// <summary>
        /// X - -
        /// O O O
        /// X - -
        /// </summary>
        [TestMethod]
        public void GetGameResult_WhenPlayerOWinsOnSecondRow_ReturnsWonByO()
        {
            // Arrange
            var board = "X--OOOX--";

            // Act
            var gameResult = this.gameResultValidator.GetGameResult(board);

            // Assert
            Assert.AreEqual(GameResult.WonByO, gameResult);
        }

        /// <summary>
        /// X - -
        /// - - X
        /// O O O
        /// </summary>
        [TestMethod]
        public void GetGameResult_WhenPlayerOWinsOnThirdRow_ReturnsWonByO()
        {
            // Arrange
            var board = "X----XOOO";

            // Act
            var gameResult = this.gameResultValidator.GetGameResult(board);

            // Assert
            Assert.AreEqual(GameResult.WonByO, gameResult);
        }

        /// <summary>
        /// O X -
        /// O - -
        /// O - X
        /// </summary>
        [TestMethod]
        public void GetGameResult_WhenPlayerOWinsOnFirstColumn_ReturnsWonByO()
        {
            // Arrange
            var board = "OX-O--O-X";

            // Act
            var gameResult = this.gameResultValidator.GetGameResult(board);

            // Assert
            Assert.AreEqual(GameResult.WonByO, gameResult);
        }

        /// <summary>
        /// X O -
        /// X O -
        /// - O X
        /// </summary>
        [TestMethod]
        public void GetGameResult_WhenPlayerOWinsOnSecondColumn_ReturnsWonByO()
        {
            // Arrange
            var board = "XO-XO--OX";

            // Act
            var gameResult = this.gameResultValidator.GetGameResult(board);

            // Assert
            Assert.AreEqual(GameResult.WonByO, gameResult);
        }

        /// <summary>
        /// X X O
        /// - - O
        /// - X O
        /// </summary>
        [TestMethod]
        public void GetGameResult_WhenPlayerOWinsOnThirdColumn_ReturnsWonByO()
        {
            // Arrange
            var board = "XXO--O-XO";

            // Act
            var gameResult = this.gameResultValidator.GetGameResult(board);

            // Assert
            Assert.AreEqual(GameResult.WonByO, gameResult);
        }

        /// <summary>
        /// X X O
        /// - O -
        /// O X -
        /// </summary>
        [TestMethod]
        public void GetGameResult_WhenPlayerOWinsByDiagonal_ReturnsWonByO()
        {
            // Arrange
            var board = "XXO-O-OX-";

            // Act
            var gameResult = this.gameResultValidator.GetGameResult(board);

            // Assert
            Assert.AreEqual(GameResult.WonByO, gameResult);
        }

        /// <summary>
        /// O X -
        /// X O -
        /// - X O
        /// </summary>
        [TestMethod]
        public void GetGameResult_WhenPlayerOWinsByDiagonal2_ReturnsWonByO()
        {
            // Arrange
            var board = "OX-XO--XO";

            // Act
            var gameResult = this.gameResultValidator.GetGameResult(board);

            // Assert
            Assert.AreEqual(GameResult.WonByO, gameResult);
        }

        /// <summary>
        /// X - O
        /// - O -
        /// X - O
        /// </summary>
        [TestMethod]
        public void GetGameResult_WhenNotFinished_ReturnsNotFinished()
        {
            // Arrange
            var board = "X-O-O-X-O";

            // Act
            var gameResult = this.gameResultValidator.GetGameResult(board);

            // Assert
            Assert.AreEqual(GameResult.NotFinished, gameResult);
        }

        /// <summary>
        /// X X O
        /// O O X
        /// X X O
        /// </summary>
        [TestMethod]
        public void GetGameResult_WhenNobodyWins_ReturnsDraw()
        {
            // Arrange
            var board = "XXOOOXXXO";

            // Act
            var gameResult = this.gameResultValidator.GetGameResult(board);

            // Assert
            Assert.AreEqual(GameResult.Draw, gameResult);
        }

        /// <summary>
        /// A A A
        /// - - X
        /// X X -
        /// </summary>
        [TestMethod]
        public void GetGameResult_WhenAnotherCharIsUsed_ReturnsInvalid()
        {
            // Arrange
            var board = "AAA--XXX-";

            // Act
            var gameResult = this.gameResultValidator.GetGameResult(board);

            // Assert
            Assert.AreEqual(GameResult.Invalid, gameResult);
        }
    }
}
