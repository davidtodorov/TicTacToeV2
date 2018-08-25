﻿using System;
using TicTacToe.Models;

namespace TicTacToe.Common.ViewModels
{

    public class GameStatusViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Board { get; set; }

        public GameState State { get; set; }

        public VisibilityType Visibility { get; set; }

        public string CreatorUsername { get; set; }

        public string CreatorUserId { get; set; }

        public string OpponentUsername { get; set; }

        public string OpponentUserId { get; set; }

        public string CurrentUserId { get; set; }

        public string PrivateJoinLink { get; set; }
    }
}
