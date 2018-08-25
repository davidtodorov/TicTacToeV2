using System;
using System.ComponentModel.DataAnnotations;
using TicTacToe.Common.Attributes;
using TicTacToe.Models;

namespace TicTacToe.Common.BindingModels
{
    public class PlayGameBindingModel
    {
        [Required]
        [NoEmptyGuid]
        public Guid GameId { get; set; }

        [Range(ValidationConstants.ROW_COL_MIN_lENGTH, ValidationConstants.ROW_COL_MAX_lENGTH)]
        public int Row { get; set; }

        [Range(ValidationConstants.ROW_COL_MIN_lENGTH, ValidationConstants.ROW_COL_MAX_lENGTH)]
        public int Col { get; set; }
    }
}
