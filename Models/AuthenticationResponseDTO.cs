using System;
using System.Collections.Generic;

namespace Models
{
    public class AuthenticationResponseDTO
    {
        public AuthenticationResponseDTO()
        {
        }

        public bool IsAuthenticationSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
        public UserDTO userDTO { get; set; }
    }
}
