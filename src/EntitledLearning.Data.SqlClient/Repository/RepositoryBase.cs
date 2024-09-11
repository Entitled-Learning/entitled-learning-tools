// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

namespace EntitledLearning.Data.Repository;

public  abstract class RepositoryBase
{
    protected string GenerateId(string? string1, string? string2)
    {
        // Generate a portion of a GUID (4 characters)
        string guidSegment = Guid.NewGuid().ToString().Substring(0, 4).ToUpper();

        string baseId = $"{string1?.Trim().Substring(0, 1)}{string2?.Trim().Substring(0, 1)}";

        // Combine the base ID with the GUID segment
        string uniqueId = $"{baseId}{guidSegment}";

        return uniqueId.ToLower();
    }
}

