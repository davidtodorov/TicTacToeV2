using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Common.ViewModels;
using TicTacToe.Data;
using TicTacToe.Models;
using TicTacToe.Services.Interfaces;

namespace TicTacToeWeb.Pages
{
    public class ScoresModel : PageModel
    {
        private readonly IScoreService scoreService;

        public ScoresModel(IScoreService scoreService)
        {
            this.scoreService = scoreService;
        }

        public IList<ScoreViewModel> Scores { get;set; }

        public void OnGet()
        {
            Scores = scoreService.GetTopTenScores();
        }
    }
}
