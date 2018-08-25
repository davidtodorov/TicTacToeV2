using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

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
