using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TicTacToe.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.RegistrationDate = DateTime.UtcNow;
            this.Scores = new List<Score>();
        }

        [Required]
        [MaxLength(ValidationConstants.NAME_MAX_LENGTH)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(ValidationConstants.NAME_MAX_LENGTH)]
        public string LastName { get; set; }

        [MaxLength(ValidationConstants.PHOTO_URL_MAX_LENGTH)]
        public string PhotoUrl { get; set; }

        public DateTime RegistrationDate { get; set; }

        public IList<Score> Scores { get; set; }

        public IList<History> History { get; set; }
    }
}
