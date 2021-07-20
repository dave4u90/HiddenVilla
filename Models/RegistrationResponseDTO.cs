using System;
using System.Collections.Generic;

namespace Models
{
    public class RegistrationResponseDTO
    {
        public RegistrationResponseDTO()
        {
        }

        public bool IsRegistrationSuccessful { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
