using System.Collections.Generic;

namespace TicTacToe.Common.ViewModels
{
    public class GameIndexViewModel
    {
        public ICollection<GameInfoViewModel> AvailableGames { get; set; }

        public ICollection<GameInfoViewModel> UserGamesInProgress { get; set; }

        public ICollection<GameInfoViewModel> UserJoinedGames { get; set; }
    }
}