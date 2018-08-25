using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TicTacToe.Common.Constants;
using TicTacToe.Models;

namespace TicTacToe.Common.BindingModels
{
    public class CreateGameBindingModel : IValidatableObject
    {
        [Required]
        [MinLength(ValidationConstants.NAME_MIN_LENGTH), MaxLength(ValidationConstants.NAME_MAX_LENGTH)]
        public string Name { get; set; }

        [MinLength(ValidationConstants.PASSWORD_MIN_LENGTH), MaxLength(ValidationConstants.PASSWORD_MAX_LENGTH)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Range(ValidationConstants.VISIBILITY_MIN_VALUE, ValidationConstants.VISIBILITY_MAX_VALUE)]
        public VisibilityType Visibility { get; set; } = VisibilityType.Public;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Visibility == VisibilityType.Protected && string.IsNullOrWhiteSpace(Password))
            {
                return new List<ValidationResult>()
                {
                    new ValidationResult("The password is required for protected games.", new[] { nameof(Password) })
                };
            }

            return Enumerable.Empty<ValidationResult>();
        }
    }
}
