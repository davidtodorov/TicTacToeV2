using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Models
{
    public enum GameState
    {
        [Display(Name = "Waiting for a second player")]
        WaitingForASecondPlayer = 1,

        [Display(Name = "X turn")]
        CreatorTurn,

        [Display(Name = "O turn")]
        OpponentTurn,

        [Display(Name = "X won")]
        CreatorVictory,

        [Display(Name = "O won")]
        OpponentVictory,

        [Display(Name = "Draw")]
        Draw
    }
}