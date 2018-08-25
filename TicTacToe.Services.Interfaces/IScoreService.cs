using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Common.ViewModels;
using TicTacToe.Models;

namespace TicTacToe.Services.Interfaces
{
    public interface IScoreService
    {
        /// <summary>
        /// Gets top 10 scores.
        /// </summary>
        /// <returns>A collection of all top 10 scores.</returns>
        IList<ScoreViewModel> GetTopTenScores();

        /// <summary>
        /// Get scores for a user by the given id;
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        ScoreViewModel GetScores(string userId);


        /// <summary>
        /// Create score for the given user.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="userId"></param>
        /// <param name="status"></param>
        void CreateScore(Game game, string userId, ScoreStatus status);

        /// <summary>
        /// Reset the scores of the user by the given id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task ResetScores(string userId);
    }
}
