using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicTacToe.Models
{
    public class History
    {
        public History()
        {
            this.Id = new Guid();
        }
        public Guid Id { get; set; }

        [Range(ValidationConstants.SCORE_STATUS_MIN_VALUE, ValidationConstants.SCORE_STATUS_MAX_VALUE)]
        public ScoreStatus Status { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public Guid GameId { get; set; }

        [ForeignKey(nameof(GameId))]
        public Game Game { get; set; }

        public DateTime Date { get; set; }
    }
}
