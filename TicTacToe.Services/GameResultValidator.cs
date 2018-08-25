using TicTacToe.Common.Constants;
using TicTacToe.Common.Enums;
using TicTacToe.Models;
using TicTacToe.Services.Interfaces;

namespace TicTacToe.Services
{
    public class GameResultValidator : IGameResultValidator
    {
        /// <inheritdoc />
        public GameResult GetGameResult(string board)
        {            
            if (board[0] == board[4] && board[4] == board[8])
            {
                // Won, diagonal (0,4,8)
                return GetWinner(board[0]);
            }            
            else if (board[2] == board[4] && board[4] == board[6])
            {
                // Won, diagonal (2,4,6)
                return GetWinner(board[2]); 
            }
            else if (board[0] == board[1] && board[1] == board[2])
            {
                // Won, row 1 (0,1,2)
                return GetWinner(board[0]);
            }
            else if (board[3] == board[4] && board[4] == board[5])
            {
                // Won, row 2 (3,4,5)
                return GetWinner(board[3]);
            }        
            else if (board[6] == board[7] && board[7] == board[8])
            {
                // Won, row 3 (6,7,8)
                return GetWinner(board[6]);
            }
            else if (board[0] == board[3] && board[3] == board[6])
            {
                // Won, column 1 (0,3,6)
                return GetWinner(board[0]);
            }
            else if (board[1] == board[4] && board[4] == board[7])
            {
                // Won, column 2 (1,4,7)
                return GetWinner(board[1]);
            }
            else if (board[2] == board[5] && board[5] == board[8])
            {
                // Won, column 3 (2, 5, 8)
                return GetWinner(board[2]);
            }            
            else if (board.Contains("-"))
            {
                // Check if it's not finished
                return GameResult.NotFinished;
            }
            else
            {                
                return GameResult.Draw;
            }       
        }

        public bool IsValidPosition(int position)
        {
            return position >= 0  && position <= 2;
        }

        public bool IsPositionTaken(string board, int position)
        {
            return board[position] != PlayerConstants.UNPLAYED_SYMBOL;
        }

        public bool IsValidGamePassword(VisibilityType visibility, string password, string gamePassword)
        {
            if (visibility == VisibilityType.Protected && password != gamePassword)
            {
                return false;
                
            }

            return true;
        }

        private GameResult GetWinner(char playerChar)
        {
            if (playerChar == PlayerConstants.X_SYMBOL)
            {
                return GameResult.WonByX;
            }
            else if (playerChar == PlayerConstants.O_SYMBOL)
            {
                return GameResult.WonByO;
            }
            else if (playerChar == PlayerConstants.UNPLAYED_SYMBOL)
            {
                return GameResult.NotFinished;
            }
            else
            {
                return GameResult.Invalid;
            }            
        }
    }
}