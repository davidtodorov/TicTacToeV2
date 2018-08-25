using System.Collections.Generic;
using TicTacToe.Common.ViewModels;
using TicTacToe.Models;

namespace TicTacToe.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Get user from the database by the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User GetUser(string userId);


        /// <summary>
        /// Gets all users from the database.
        /// </summary>
        /// <returns>A collection with users from the database.</returns>
        IList<UserInfoViewModel> GetAllUsers();

        UserDetailsViewModel GetUserDetails(string userId);

        /// <summary>
        /// Registers a user by the given parameters.
        /// </summary>
        /// <param name="input">The user's input data.</param>
        /// <returns>The created user's data.</returns>
        //UserInfoOutput Register(UserRegistrationInput input);
    }
}