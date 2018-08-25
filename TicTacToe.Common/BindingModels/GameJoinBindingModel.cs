using System;
using System.ComponentModel.DataAnnotations;
using TicTacToe.Common.Attributes;

namespace TicTacToe.Common.BindingModels
{
    public class GameJoinBindingModel
    {
        [Required]
        [NoEmptyGuid]
        public Guid GameId { get; set; }

        public string Password { get; set; }
    }
}
