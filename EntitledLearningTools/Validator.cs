using System;
using System.Collections.Generic;

public static class Validator
{
    public static string ValidateStudentFile(Dictionary<string, string> csvRecord)
    {
        // Validate first_name column
        if (csvRecord.TryGetValue("first_name", out var firstName) && !string.IsNullOrWhiteSpace(firstName) && firstName.Contains(" "))
        {
            return "Invalid value in the 'first_name' column. It should not contain spaces.";
        }

        // Add more validations for other columns if needed

        return string.Empty;
    }

    public static string ValidateCommunityPartnerFile(Dictionary<string, string> csvRecord)
    {
        // Validate phone_number column
        //if (csvRecord.TryGetValue("Phone", out var phoneNumber) && !IsValidPhoneNumber(phoneNumber))
        //{
        //    return "Invalid value in the 'phone_number' column. Please provide a valid phone number format.";
        //}

        // Add more validations for other columns if needed

        return string.Empty;
    }

    // Additional utility methods for validations

    private static bool IsValidPhoneNumber(string phoneNumber)
    {
        // Basic validation for illustration purposes
        // You might want to implement a more robust phone number validation
        return phoneNumber?.All(char.IsDigit) ?? false;
    }
}
