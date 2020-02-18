﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HomeAssets.Models.Attributes
{
    public class ValidEmailDomain : ValidationAttribute
    {
        private readonly List<string> validVendors;

        public ValidEmailDomain()
        {
            validVendors = new List<string>()
            {
                "mailinator.com",
                "simulator.amazonses.com",
                "gmail.com",
                "outlook.com",
                "hotmail.com",
                "yahoo.com",
                "protonmail.com"
            };

            ErrorMessage = "Dominios validos: ";

            for (int i = 1; i < validVendors.Count(); i++)
            {
                ErrorMessage += $"@{validVendors[i]}, ";
            }
        }

        public override bool IsValid(object value)
        {
            string valueVendor = value.ToString().Split('@')[1].ToLower();

            if (validVendors.Find(vendor => vendor == valueVendor) != null)
            {
                return true;
            }
            return false;
        }
    }
}