using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Common.Constants
{
    public static class ErrorMessagesConstants
    {
        public const string GAME_IS_NULL = "Game cannot be null";
        public const string USERID_IS_NULL = "UserId cannot be null";
        public const string GAME_NOT_FOUND = "The game cannot be found.";
        public const string PASS_DOESNT_MATCH = "Input password doesn't match";
        public const string CANT_FIND_GAME_ID = "The game with identifier: '{0}' was not found.";
        public const string POSITION_IS_TAKEN = "The position is already taken!";
        public const string NOT_YOUR_TURN = "It's not your turn";
        public const string HISTORY_NOT_FOUND = "History not found";
        public const string INVALID_ROW = "The input row: {0} is invalid";
        public const string INVALID_COL = "The input col: {0} is invalid";
    }
}
