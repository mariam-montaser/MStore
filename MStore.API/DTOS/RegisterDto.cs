using System.ComponentModel.DataAnnotations;

namespace MStore.API.DTOS
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        //[RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,10}$", ErrorMessage = "Password must has at least 1 lower, 1 upper, 1 number, and 1 sympol")]
        public string Password { get; set; }

    }
}
