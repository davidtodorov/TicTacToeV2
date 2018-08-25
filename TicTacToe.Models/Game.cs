using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicTacToe.Models
{
    public class Game
    {
        public Game()
        {
            this.Board = "---------";
            this.CreationDate = DateTime.UtcNow;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid GameId { get; set; }

        [Required]
        [MinLength(ValidationConstants.NAME_MIN_LENGTH)]
        [MaxLength(ValidationConstants.NAME_MAX_LENGTH)]
        public string Name { get; set; }

        [Required]
        [StringLength(ValidationConstants.GAMEBOARD_LENGTH, MinimumLength = ValidationConstants.GAMEBOARD_LENGTH)]
        public string Board { get; set; }

        [Range(ValidationConstants.PASSWORD_MIN_LENGTH, ValidationConstants.PASSWORD_MAX_LENGTH)]
        public string HashedPassword { get; set; }

        public DateTime CreationDate { get; set; }

        [Required]
        [Range(ValidationConstants.VISIBILITY_MIN_VALUE, ValidationConstants.VISIBILITY_MAX_VALUE)]
        public VisibilityType Visibility { get; set; }

        [Range(ValidationConstants.GAMESTATE_MIN_VALUE, ValidationConstants.GAMESTATE_MAX_VALUE)]
        public GameState State { get; set; }

        [Required]
        public string CreatorUserId { get; set; }

        [ForeignKey(nameof(CreatorUserId))]
        public User CreatorUser { get; set; }

        public string OpponentUserId { get; set; }

        [ForeignKey(nameof(OpponentUserId))]
        public User OpponentUser { get; set; }
    }
}
