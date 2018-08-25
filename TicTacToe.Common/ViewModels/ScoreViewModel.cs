namespace TicTacToe.Common.ViewModels
{
    public class ScoreViewModel
    {
        public string UserId { get; set; }

        public string Username { get; set; }

        public int Wins { get; set; }

        public int Loses { get; set; }

        public int Draws { get; set; }

        public int Points => Wins * 100 + Draws * 30 + Loses * 15;
    }
}
