using System;
using TicTacToe.Models;

namespace TicTacToe.Common.ViewModels
{
    public class HistoryViewModel
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public string OppononetUsername { get; set; }

        public string GameName { get; set; }

        public ScoreStatus ScoreStatus { get; set; }

        public DateTime Date { get; set; }
    }
}
