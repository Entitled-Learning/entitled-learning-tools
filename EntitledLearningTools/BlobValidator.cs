using System.Text.RegularExpressions;

namespace EntitledLearningTools;

public static class BlobValidator
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
        //Validate phone_number column
        if (csvRecord.TryGetValue("Phone", out var phoneNumber) && !IsValidPhoneNumber(phoneNumber))
        {
            return "Invalid value in the 'Phone' column. Please provide a valid phone number format.";
        }

        //Add more validations for other columns if needed

        return string.Empty;
    }

    // Additional utility methods for validations

    private static bool IsValidPhoneNumber(string phoneNumber)
    {
        // Define a regular expression pattern for a valid phone number
        string pattern = @"^\+?(\d[\d-. ]+)?(\(\d+\))?[\d-. ]+\d$";

        // Create a regular expression object
        Regex regex = new Regex(pattern);

        // Use the regular expression to match the input phone number
        Match match = regex.Match(phoneNumber);

        // Return true if the input phone number matches the pattern
        return match.Success;
    }
}
