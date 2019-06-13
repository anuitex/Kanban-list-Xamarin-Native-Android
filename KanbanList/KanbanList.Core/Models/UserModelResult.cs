using System.ComponentModel.DataAnnotations;

namespace KanbanList.Core.Models
{
    public class UserModelResult
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "First name isn't valid")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name isn't valid")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression("^\\S+@\\S+$", ErrorMessage = "Email isn't valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password isn't valid")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,15}$", ErrorMessage = "Password isn't corrected")]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Confirm password isn't compare")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Organization name isn't valid")]
        public string OrganizationName { get; set; }
    }
}
