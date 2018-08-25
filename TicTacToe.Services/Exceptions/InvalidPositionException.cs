using System;

namespace TicTacToe.Services.Exceptions
{
    public class InvalidPositionException : Exception
    {
        public InvalidPositionException()
        {
        }

        public InvalidPositionException(string message)
            : base(message)
        {
        }

        public InvalidPositionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
