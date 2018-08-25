using System;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Data;

namespace TicTacToe.Tests.Mocks
{
    public class MockDbContext
    {
        public static TicTacToeDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<TicTacToeDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new TicTacToeDbContext(options);
        }
    }
}
