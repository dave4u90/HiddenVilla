using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class AuthenticationDTO
    {
        public AuthenticationDTO()
        {
        }

        [Required(ErrorMessage = "User name is required")]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email address")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
