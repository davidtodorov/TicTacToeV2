using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Models
{
    public enum VisibilityType
    {
        [Display(Name = "Public")]
        Public = 1,

        [Display(Name = "Private")]
        Private,

        [Display(Name = "Protected")]
        Protected
    }
}