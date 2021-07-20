using System;
namespace HiddenVilla_Api.Helper
{
    public class APISettings
    {
        public APISettings()
        {
        }

        public string SecretKey { get; set; }
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
    }
}
