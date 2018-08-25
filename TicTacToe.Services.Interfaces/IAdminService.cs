using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Services.Interfaces
{
    public interface IAdminService
    {
        /// <summary>
        /// Lock a user by the given id.
        /// </summary>
        /// <param name="userId"></param>
        Task LockUser(string userId);


        /// <summary>
        /// Unlock a user by the given id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task UnlockUser(string userId);
    }
}
