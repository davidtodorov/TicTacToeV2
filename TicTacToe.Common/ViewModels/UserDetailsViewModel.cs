using System.Collections.Generic;

namespace TicTacToe.Common.ViewModels
{
    public class UserDetailsViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IList<ScoreViewModel> Scores { get; set; }

        public IList<HistoryViewModel> History { get; set; }
        
    }
}
