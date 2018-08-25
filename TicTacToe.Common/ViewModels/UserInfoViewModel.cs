using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Common.ViewModels
{
    public class UserInfoViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime RegistrationDate { get; set; }

        public bool IsLocked { get; set; }
    }
}
