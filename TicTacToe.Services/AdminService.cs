using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TicTacToe.Data;
using TicTacToe.Models;
using TicTacToe.Services.Interfaces;

namespace TicTacToe.Services
{
    public class AdminService : IAdminService
    {
        private readonly TicTacToeDbContext context;
        private readonly UserManager<User> userManager;

        public AdminService(TicTacToeDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task LockUser(string userId)
        {
            var user = this.context.Users.FirstOrDefault(u => u.Id == userId);
            await this.userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddYears(1000));
        }

        public async Task UnlockUser(string userId)
        {
            var user = this.context.Users.FirstOrDefault(u => u.Id == userId);
            await this.userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddDays(-1));

        }
    }
}

