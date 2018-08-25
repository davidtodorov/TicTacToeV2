using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Common.Constants;
using TicTacToe.Common.ViewModels;
using TicTacToe.Data;
using TicTacToe.Models;
using TicTacToe.Services.Interfaces;
using TicTacToe.Services.Mappings;

namespace TicTacToe.Services
{
    public class UserService : IUserService, IDisposable
    {
        private readonly TicTacToeDbContext context;
        private readonly IScoreService scoreService;
        private readonly IHistoryService historyService;

        public UserService(TicTacToeDbContext context, IScoreService scoreService, IHistoryService historyService)
        {
            this.context = context;
            this.scoreService = scoreService;
            this.historyService = historyService;
        }

        public User GetUser(string userId)
        {
            return this.context.Users.FirstOrDefault(u => u.Id == userId);
        }

        public IList<UserInfoViewModel> GetAllUsers()
        {
            var users = this.context.Users.Select(UserMappings.ToUserInfoViewModel).ToList();
            return users;
        }

        public UserDetailsViewModel GetUserDetails(string userId)
        {
            // todo: check controller for this if user want to see details
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException(ErrorMessagesConstants.USERID_IS_NULL);
            }
            var user = this.context.Users.Where(u => u.Id == userId).Select(UserMappings.ToUserDetailsViewModel).FirstOrDefault();
            var scoresModel = this.scoreService.GetScores(userId);

            // it must be list so display template can be reused
            var scoreListModel = new List<ScoreViewModel>() {scoresModel};
            user.Scores = scoreListModel;

            var historyModel = this.historyService.GetHistory(userId);
            user.History = historyModel;

            return user;
        }
        
        public void Dispose()
        {
            this.context?.Dispose();
        }
    }
}