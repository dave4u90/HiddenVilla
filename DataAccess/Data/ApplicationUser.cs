﻿using System;
using Common;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string CreatedBy { get; set; } = SD.HiddenVilla_Client;
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; } = SD.HiddenVilla_Client;
        public DateTime UpdatedDate { get; set; }
        public string DeletedBy { get; set; } = SD.Role_Admin;
        public DateTime DeletedDate { get; set; }
    }
}
