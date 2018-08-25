using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TicTacToe.Common.Attributes;

namespace TicTacToe.Common.BindingModels
{
    public class DeleteHisotryBindingModel
    {
        [Required]
        [NoEmptyGuid]
        public Guid Id { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
