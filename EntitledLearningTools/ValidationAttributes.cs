using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace EntitledLearningTools;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class ValidEmailOrEmptyAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            return true; // Null values are allowed
        }

        var stringValue = value as string;

        if (string.IsNullOrWhiteSpace(stringValue))
        {
            return true; // Empty or whitespace values are allowed
        }

        try
        {
            var addr = new System.Net.Mail.MailAddress(stringValue);
            return addr.Address == stringValue;
        }
        catch
        {
            return false; // Invalid email format
        }
    }
}

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class ValidPhoneNumberOrEmptyAttribute : ValidationAttribute
    {
        private const string PhoneNumberRegex = @"^\+(?:[0-9] ?){6,14}[0-9]$";

        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return true; // Null values are allowed
            }

            var stringValue = value as string;

            if (string.IsNullOrWhiteSpace(stringValue))
            {
                return true; // Empty or whitespace values are allowed
            }

            return Regex.IsMatch(stringValue, PhoneNumberRegex);
        }
    }
