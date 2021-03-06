﻿using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicTacToe.Common.ViewModels;
using TicTacToe.Services.Interfaces;

namespace TicTacToeWeb.Pages
{
    public class HistoryModel : PageModel
    {
        private readonly IHistoryService historyService;

        public HistoryModel(IHistoryService historyService)
        {
            this.historyService = historyService;
        }
        public IList<HistoryViewModel> History { get; set; }

        public int Count { get; set; }

        public void OnGet()
        {
           var userId = this.User.Identity.GetUserId();
           this.History = historyService.GetHistory(this.User.Identity.GetUserId());
        }
    }
}