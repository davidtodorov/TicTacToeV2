using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Common.BindingModels;
using TicTacToe.Common.Constants;
using TicTacToe.Common.Enums;
using TicTacToe.Common.ViewModels;
using TicTacToe.Data;
using TicTacToe.Models;
using TicTacToe.Services.Exceptions;
using TicTacToe.Services.Interfaces;
using TicTacToe.Services.Mappings;

namespace TicTacToe.Services
{
    public class GameService : IGameService
    {
        private readonly TicTacToeDbContext context;
        private readonly IGameResultValidator gameValidator;
        private readonly IScoreService scoreService;
        private readonly IHistoryService historyService;
        private readonly Random randomGenerator;


        public GameService(TicTacToeDbContext context, IGameResultValidator gameValidator)
        {
            this.context = context;
            this.gameValidator = gameValidator;
            this.randomGenerator = new Random();
        }

        /// <inheritdoc />
        public ICollection<GameInfoViewModel> GetAvailableGames(string userId)
        {
            Expression<Func<Game, bool>> expression = x => x.State == GameState.WaitingForASecondPlayer 
                                                           && x.CreatorUserId != userId 
                                                           && (x.Visibility == VisibilityType.Public || x.Visibility == VisibilityType.Protected);
            return GetGames(expression); 
        }

        /// <inheritdoc />
        public ICollection<GameInfoViewModel> GetUserGamesInProgress(string userId)
        {
            Expression<Func<Game, bool>> expression = x => (x.State == GameState.WaitingForASecondPlayer || x.State == GameState.CreatorTurn || x.State == GameState.OpponentTurn) 
                                                           && x.CreatorUserId == userId;
            return GetGames(expression);
        }

        public ICollection<GameInfoViewModel> GetUserJoinedGames(string userId)
        {
            Expression<Func<Game, bool>> expression = x => (x.State == GameState.WaitingForASecondPlayer || x.State == GameState.CreatorTurn || x.State == GameState.OpponentTurn) 
                                                           && x.OpponentUserId == userId;
            return GetGames(expression);
        }

        /// <inheritdoc />
        public GameStatusViewModel Create(CreateGameBindingModel input, string creatorUserId)
        {
            if (string.IsNullOrWhiteSpace(creatorUserId))
            {
                throw new ValidationException(ErrorMessagesConstants.USERID_IS_NULL);
            }

            var game = new Game()
            {
                Name = input.Name,
                Visibility = input.Visibility,
                HashedPassword = input.Password,
                CreatorUserId = creatorUserId,
                State = GameState.WaitingForASecondPlayer
            };

            context.Games.Add(game);
            context.SaveChanges();

            return game.ToGameStatus();
        }

        /// <inheritdoc />
        public GameStatusViewModel Join(GameJoinBindingModel input, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ValidationException(ErrorMessagesConstants.USERID_IS_NULL);
            }

            var game = this.context.Games
                .Where(x => x.State == GameState.WaitingForASecondPlayer && x.CreatorUserId != userId)
                .Include(g => g.CreatorUser)
                .Include(g => g.OpponentUser)
                .FirstOrDefault(g => g.GameId == input.GameId);

            if (game == null)
            {
                throw new NotFoundException(ErrorMessagesConstants.GAME_NOT_FOUND);
            }

            if (!gameValidator.IsValidGamePassword(game.Visibility, input.Password, game.HashedPassword))
            {
                throw new ValidationException(ErrorMessagesConstants.PASS_DOESNT_MATCH);
            }

            var randNum = randomGenerator.Next(0, 2);
            game.State = randNum == 0 ? GameState.CreatorTurn : GameState.OpponentTurn;
            game.OpponentUserId = userId;

            context.SaveChanges();

            return game.ToGameStatus();
        }

        /// <inheritdoc />
        public GameStatusViewModel Status(Guid gameId, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ValidationException(ErrorMessagesConstants.USERID_IS_NULL);
            }

            var gameStatus = this.context.Games.Select(GameMappings.ToGameStatusViewModel)
                                         .FirstOrDefault(g => g.Id == gameId && (userId == g.CreatorUserId || userId == g.OpponentUserId));

            gameStatus.CurrentUserId = userId;

            if (gameStatus == null)
            {
                throw new NotFoundException(String.Format(ErrorMessagesConstants.CANT_FIND_GAME_ID, gameId));
            }
            
            return gameStatus;
        }

        /// <inheritdoc />
        public GameStatusViewModel Play(Guid gameId, string userId, int row, int col)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ValidationException(ErrorMessagesConstants.USERID_IS_NULL);
            }

            var game = this.context.Games
                .Include(g => g.CreatorUser)
                .Include(x => x.OpponentUser)
                .Where(g => g.CreatorUserId == userId || g.OpponentUserId == userId)
                .Where(g => g.State == GameState.CreatorTurn || g.State == GameState.OpponentTurn)
                .FirstOrDefault(g => g.GameId == gameId);

            if (game == null)
            {
                throw new NotFoundException(String.Format(ErrorMessagesConstants.CANT_FIND_GAME_ID, gameId));
            }
            
            if (game.State == GameState.CreatorTurn && game.CreatorUserId != userId
                || game.State == GameState.OpponentTurn && game.OpponentUserId != userId)
            {
                throw new ValidationException(ErrorMessagesConstants.NOT_YOUR_TURN);
            }

            char playerChar = game.State == GameState.CreatorTurn ? PlayerConstants.X_SYMBOL : PlayerConstants.O_SYMBOL;

            var boardArray = game.Board.ToCharArray();

            if (!gameValidator.IsValidPosition(row))
            {
                throw new InvalidPositionException(String.Format(ErrorMessagesConstants.INVALID_ROW, row));
            }

            if (!gameValidator.IsValidPosition(col))
            {
                throw new InvalidPositionException(String.Format(ErrorMessagesConstants.INVALID_ROW, col));
            }

            var position = 3 * row + col;
  
            var isTaken = gameValidator.IsPositionTaken(game.Board, position);
            if (isTaken)
            {
                throw new ValidationException(ErrorMessagesConstants.POSITION_IS_TAKEN);
            }

            boardArray[position] = playerChar;
            var boardInsertedPosition = string.Join(string.Empty, boardArray);
            var gameResult = gameValidator.GetGameResult(boardInsertedPosition);

            game.Board = boardInsertedPosition;
            
            this.CheckGameResult(gameResult, game);
            
            context.SaveChanges();
            return game.ToGameStatus();
        }
 
        private void CheckGameResult(GameResult gameResult, Game game)
        {
            if (game == null)
            {
                throw new InvalidOperationException(ErrorMessagesConstants.GAME_IS_NULL);
            }

            if (gameResult == GameResult.NotFinished)
            {
                //change turn
                game.State = game.State == GameState.CreatorTurn ? GameState.OpponentTurn : GameState.CreatorTurn;
            }
            else if (gameResult == GameResult.WonByX)
            {
                game.State = GameState.CreatorVictory;
                this.CreateScoreAndHistory(game, game.CreatorUserId, ScoreStatus.Win);
                this.CreateScoreAndHistory(game, game.OpponentUserId, ScoreStatus.Loss);
            }
            else if (gameResult == GameResult.WonByO)
            {
                game.State = GameState.OpponentVictory;
                this.CreateScoreAndHistory(game, game.CreatorUserId, ScoreStatus.Loss);
                this.CreateScoreAndHistory(game, game.OpponentUserId, ScoreStatus.Win);
            }
            else if (gameResult == GameResult.Draw)
            {
                game.State = GameState.Draw;
                this.CreateScoreAndHistory(game, game.CreatorUserId, ScoreStatus.Draw);
                this.CreateScoreAndHistory(game, game.OpponentUserId, ScoreStatus.Draw);
            }
        }

        private void CreateScoreAndHistory(Game game, string userId, ScoreStatus status)
        {
            var score = new Score()
            {
                GameId = game.GameId,
                UserId = userId,
                Status = status
            };
            var history = new History()
            {
                GameId = game.GameId,
                UserId = userId,
                Status = status,
                Date = DateTime.Now
            };

            context.Scores.Add(score);
            context.Histories.Add(history);
        }

        private ICollection<GameInfoViewModel> GetGames(Expression<Func<Game, bool>> expression)
        {
            return this.context.Games
                .Where(expression)
                .OrderByDescending(x => x.CreationDate)
                .Select(GameMappings.ToGameInfoViewModel)
                .ToList();
        }
    }
}