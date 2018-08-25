using TicTacToe.Common.Enums;
using TicTacToe.Models;

namespace TicTacToe.Services.Interfaces
{
    public interface IGameResultValidator
    {
        /// <summary>
        /// Gets the game result by provided coordinates.
        /// </summary>
        /// <param name="board">The game's board.</param>
        /// <returns>The game result indicating whether the result is not finished, won by X or O, draw, etc.</returns>
        GameResult GetGameResult(string board);

        /// <summary>
        /// Check if the given position is valid.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        bool IsValidPosition(int position);


        /// <summary>
        /// Check if the given position is taken.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        bool IsPositionTaken(string board, int position);


        /// <summary>
        /// Check if the password is correct
        /// </summary>
        /// <param name="visibility"></param>
        /// <param name="password"></param>
        /// <param name="gamePassword"></param>
        /// <returns></returns>
        bool IsValidGamePassword(VisibilityType visibility, string password, string gamePassword);
    }
}