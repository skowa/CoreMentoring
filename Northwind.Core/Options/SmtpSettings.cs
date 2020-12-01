﻿namespace Northwind.Core.Options
{
    public class SmtpSettings
    {
        public string Server { get; set; }

        public int? Port { get; set; }

        public string SenderEmail { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
