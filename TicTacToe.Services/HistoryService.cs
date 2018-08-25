using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Common.Constants;
using TicTacToe.Common.ViewModels;
using TicTacToe.Data;
using TicTacToe.Services.Interfaces;
using TicTacToe.Services.Mappings;

namespace TicTacToe.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly TicTacToeDbContext context;

        public HistoryService(TicTacToeDbContext context)
        {
            this.context = context;
        }

        public IList<HistoryViewModel> GetHistory(string userId)
        {
            var model = context.Histories.Where(h => h.UserId == userId).Select(GameMappings.ToHistoryViewModel)
                .OrderByDescending(m => m.Date)
                .ToList();

            return model;
        }
        
        public void DeleteHistory(Guid id, string userId)
        {
            var history = context.Histories.SingleOrDefault(h => h.Id == id && h.UserId == userId);
            if (history == null)
            {
                throw new ArgumentNullException(ErrorMessagesConstants.HISTORY_NOT_FOUND);
            }

            context.Histories.Remove(history);
            context.SaveChanges();
        }

        public void DeleteAllHistory(string userId)
        {
            var histories = context.Histories.Where(h => h.UserId == userId);
            if (!histories.Any())
            {
                throw new ArgumentNullException(ErrorMessagesConstants.HISTORY_NOT_FOUND);
            }
            context.Histories.RemoveRange(histories);
            context.SaveChanges();
        }
    }
}
