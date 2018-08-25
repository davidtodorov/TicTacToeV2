using System;
using System.Collections.Generic;
using TicTacToe.Common.BindingModels;
using TicTacToe.Common.ViewModels;

namespace TicTacToe.Services.Interfaces
{
    public interface IGameService
    {
        /// <summary>
        /// Gets all available games waiting for an opponent.
        /// </summary>
        /// <param name="userId">The user's identifier searching for a game.</param>
        /// <returns>A collection of all available games.</returns>
        ICollection<GameInfoViewModel> GetAvailableGames(string userId);

        /// <summary>
        /// Gets all users' games.
        /// </summary>
        /// <param name="userId">The user's identifier searching for a game.</param>
        /// <returns>A collection of all available games.</returns>
        ICollection<GameInfoViewModel> GetUserGamesInProgress(string userId);

        /// <summary>
        /// Gets all joined users' games.
        /// </summary>
        /// <param name="userId">The user's identifier searching for a game.</param>
        /// <returns>A collection of all available games.</returns>
        ICollection<GameInfoViewModel> GetUserJoinedGames(string userId);

        /// <summary>
        /// Creates a new game session by given name.
        /// </summary>
        /// <param name="input">The game's input information.</param>
        /// <param name="creatorUserId">The creator user's identifier.</param>
        /// <returns>The status information about the game session.</returns>
        GameStatusViewModel Create(CreateGameBindingModel input, string creatorUserId);

        /// <summary>
        /// Joins to a game session by given game's identifier.
        /// </summary>
        /// <param name="input">The game's input information.</param>
        /// <param name="userId">The user's identifier.</param>
        /// <returns>The status information about the game session.</returns>
        GameStatusViewModel Join(GameJoinBindingModel input, string userId);

        /// <summary>
        /// Gets a status information about a game session.
        /// </summary>
        /// <param name="gameId">The game's identifier.</param>
        /// <param name="userId">The user's identifier requesting the status.</param>
        /// <returns>The status information about the game session.</returns>
        GameStatusViewModel Status(Guid gameId, string userId);

        /// <summary>
        /// Plays a turn for a given game session.
        /// </summary>
        /// <param name="gameId">The game's identifier.</param>
        /// <param name="userId">The user's identifier playing a turn.</param>
        /// <param name="row">The specified row.</param>
        /// <param name="col">The specified column.</param>
        /// <returns>The status information about the game session.</returns>
        GameStatusViewModel Play(Guid gameId, string userId, int row, int col);
    }
}