﻿namespace HomeAssets.Domain.Models
{
    public class SmtpSettings
    {
        public string From { get; set; }
        public string Name { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}