﻿using System;
namespace HiddenVilla_Api.Helper
{
    public class MailJetSettings
    {
        public MailJetSettings()
        {
        }

        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public string Email { get; set; }
    }
}