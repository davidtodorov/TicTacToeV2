using System;
using System.Linq;
using System.Linq.Expressions;
using TicTacToe.Common.ViewModels;
using TicTacToe.Models;

namespace TicTacToe.Services.Mappings
{
    internal static class UserMappings
    {
        public static readonly Expression<Func<User, UserInfoViewModel>> ToUserInfoViewModel =
            entity => new UserInfoViewModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                RegistrationDate = entity.RegistrationDate,
                IsLocked = entity.LockoutEnd.Value > DateTimeOffset.Now
            };

        public static readonly Expression<Func<User, UserDetailsViewModel>> ToUserDetailsViewModel =
            entity => new UserDetailsViewModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName
            };
    }
}