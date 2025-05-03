// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using System.Text.RegularExpressions;

namespace EntitledLearning.Tools.UI.Utilities;

public static class NameParser
{
    private static readonly string[] CommonPrefixes = { "dr", "mr", "ms", "mrs", "miss", "prof", "rev" };
    private static readonly string[] CommonSuffixes = { "jr", "sr", "ii", "iii", "iv", "v", "md", "phd", "esq" };

    public static (string? prefix, string firstName, string? middleName, string lastName, string? suffix) ParseFullName(string? fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            return (null, string.Empty, null, string.Empty, null);
        }

        // Trim and normalize the input
        fullName = fullName.Trim();
        
        // Extract prefix if present
        string? prefix = null;
        foreach (var p in CommonPrefixes)
        {
            string prefixPattern = $"^({p}\\.|{p})\\s+";
            var prefixMatch = Regex.Match(fullName.ToLower(), prefixPattern, RegexOptions.IgnoreCase);
            if (prefixMatch.Success)
            {
                prefix = prefixMatch.Groups[1].Value;
                fullName = fullName.Substring(prefixMatch.Length).Trim();
                break;
            }
        }

        // Extract suffix if present
        string? suffix = null;
        foreach (var s in CommonSuffixes)
        {
            string suffixPattern = $",?\\s+({s}\\.?|{s})$";
            var suffixMatch = Regex.Match(fullName.ToLower(), suffixPattern, RegexOptions.IgnoreCase);
            if (suffixMatch.Success)
            {
                suffix = suffixMatch.Groups[1].Value;
                fullName = fullName.Substring(0, fullName.Length - suffixMatch.Length).Trim();
                break;
            }
        }

        // Split remaining name into parts
        var nameParts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        // Handle different cases based on number of parts
        switch (nameParts.Length)
        {
            case 0:
                return (prefix, string.Empty, null, string.Empty, suffix);
            
            case 1:
                return (prefix, nameParts[0], null, nameParts[0], suffix);
            
            case 2:
                return (prefix, nameParts[0], null, nameParts[1], suffix);
            
            default:
                // First part is the first name
                var firstName = nameParts[0];
                
                // Last part is the last name
                var lastName = nameParts[^1];
                
                // Middle parts make up the middle name(s)
                var middleName = string.Join(" ", nameParts.Skip(1).Take(nameParts.Length - 2));
                
                return (prefix, firstName, middleName, lastName, suffix);
        }
    }
}