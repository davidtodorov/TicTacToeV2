using System;
using System.Collections.Generic;
using TicTacToe.Common.ViewModels;
using TicTacToe.Models;

namespace TicTacToe.Services.Interfaces
{
    public interface IHistoryService
    {
        IList<HistoryViewModel> GetHistory(string userId);

        void DeleteHistory(Guid id, string userId);

        void DeleteAllHistory(string userId);
    }
}
