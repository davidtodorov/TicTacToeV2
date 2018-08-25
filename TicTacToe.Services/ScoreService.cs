using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Common.Constants;
using TicTacToe.Common.ViewModels;
using TicTacToe.Data;
using TicTacToe.Models;
using TicTacToe.Services.Interfaces;
using TicTacToe.Services.Mappings;

namespace TicTacToe.Services
{
    public class ScoreService : IScoreService
    {
        private readonly TicTacToeDbContext context;

        public ScoreService(TicTacToeDbContext context)
        {
            this.context = context;
        }

        public IList<ScoreViewModel> GetTopTenScores()
        {
            var scores = this.context.Users.AsNoTracking()
                .Select(GameMappings.ToScoreViewModel)
                .OrderByDescending(s => s.Wins)
                .Take(10)
                .ToList();

            return scores;
        }

        public ScoreViewModel GetScores(string userId)
        {
            var scores = this.context
                .Users.Where(u => u.Id == userId)
                .Select(GameMappings.ToScoreViewModel).FirstOrDefault();

            return scores;
        }

        public void CreateScore(Game game, string userId, ScoreStatus status)
        {
            var score = new Score()
            {
                GameId = game.GameId,
                UserId = userId,
                Status = status
            };

            context.Scores.Add(score);
        }

        public async Task ResetScores(string userId)
        {
            if (userId == null) 
            {
                throw new ArgumentNullException(ErrorMessagesConstants.USERID_IS_NULL);
            }
            var userScores = this.context.Scores.Where(s => s.UserId == userId);
            foreach (var score in userScores)
            {
                context.Scores.RemoveRange(score);
            }
            await context.SaveChangesAsync();
        }
    }
}

