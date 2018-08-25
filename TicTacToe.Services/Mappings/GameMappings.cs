using System;
using System.Linq;
using System.Linq.Expressions;
using TicTacToe.Common.ViewModels;
using TicTacToe.Models;

namespace TicTacToe.Services.Mappings
{
    internal static class GameMappings
    {
        public static readonly Expression<Func<Game, GameInfoViewModel>> ToGameInfoViewModel =
            entity => new GameInfoViewModel
            {
                Id = entity.GameId,
                Name = entity.Name,
                CreationDate = entity.CreationDate,
                CreatorUsername = entity.CreatorUser != null ? entity.CreatorUser.FirstName : null,
                CreatorUserId = entity.CreatorUserId,
                OpponentUsername = entity.OpponentUser != null ? entity.OpponentUser.FirstName : null,
                OpponentUserId = entity.OpponentUserId,
                State = entity.State,
                Visibility = entity.Visibility
            };

        public static readonly Expression<Func<Game, GameStatusViewModel>> ToGameStatusViewModel =
            entity => new GameStatusViewModel()
            {
                Id = entity.GameId,
                Name = entity.Name,
                CreatorUserId = entity.CreatorUserId,
                OpponentUserId = entity.OpponentUserId,
                CreatorUsername = entity.CreatorUser != null ? entity.CreatorUser.FirstName : null,
                OpponentUsername = entity.OpponentUser != null ? entity.OpponentUser.FirstName : null,
                Board = entity.Board,
                State = entity.State,
                Visibility = entity.Visibility
            };

        public static readonly Expression<Func<User, ScoreViewModel>> ToScoreViewModel =
            entity => new ScoreViewModel()
            {
                UserId = entity.Id,
                Username = entity.Email,
                Wins = entity.Scores.Count(s => s.Status == ScoreStatus.Win),
                Loses = entity.Scores.Count(s => s.Status == ScoreStatus.Loss),
                Draws = entity.Scores.Count(s => s.Status == ScoreStatus.Draw)
            };

        public static GameStatusViewModel ToGameStatus(this Game entity)
        {
            return new GameStatusViewModel()
            {
                Id = entity.GameId,
                Name = entity.Name,
                CreatorUserId = entity.CreatorUserId,
                OpponentUserId = entity.OpponentUserId,
                CreatorUsername = entity.CreatorUser?.FirstName,
                OpponentUsername = entity.OpponentUser?.FirstName,
                Board = entity.Board,
                State = entity.State, 
                Visibility = entity.Visibility,
            };
        }

        public static readonly Expression<Func<History, HistoryViewModel>> ToHistoryViewModel = entity => new HistoryViewModel()
        {
            Id = entity.Id,
            UserId = entity.UserId,
            ScoreStatus = entity.Status,
            GameName = entity.Game.Name,
            OppononetUsername = entity.Game.CreatorUserId == entity.UserId ? entity.Game.OpponentUser.FirstName : entity.Game.CreatorUser.FirstName,
            Date = entity.Date
        };
    }
}