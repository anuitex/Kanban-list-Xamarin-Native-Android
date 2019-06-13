using System.ComponentModel.DataAnnotations;

namespace KanbanList.Core.Models
{
    public class LoginModelResult
    {
        [Required]
        [RegularExpression("^\\S+@\\S+$", ErrorMessage = "Username isn't valid")]
        public string Username { get; set; }

        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,15}$", ErrorMessage = "Password isn't corrected")]
        public string Password { get; set; }
    }
}
