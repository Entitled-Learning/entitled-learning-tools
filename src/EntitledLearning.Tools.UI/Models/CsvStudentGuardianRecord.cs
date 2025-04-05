   // ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

namespace EntitledLearning.Tools.UI.Models;

public record CsvStudentGuardianRecord
{
    // Student Properties
    public string StudentFirstName { get; set; } = string.Empty;
    public string StudentLastName { get; set; } = string.Empty;
    public string? StudentMiddleName { get; set; }
    public string? StudentPrefix { get; set; }
    public string? StudentSuffix { get; set; }
    public string? StudentRace { get; set; }
    public DateTime? StudentDateOfBirth { get; set; }
    public string? StudentHouseholdIncomeRange { get; set; }
    public string? StudentShirtSize { get; set; }
    public bool StudentAllowPhotoRelease { get; set; }
    public string? StudentEmailAddress { get; set; }
    public string? StudentAddressLine1 { get; set; }
    public string? StudentAddressLine2 { get; set; }
    public string? StudentCity { get; set; }
    public string? StudentState { get; set; }
    public string? StudentZipCode { get; set; }

    // Guardian Properties
    public string? GuardianFirstName { get; set; }
    public string? GuardianLastName { get; set; }
    public string? GuardianMiddleName { get; set; }
    public string? GuardianPrefix { get; set; }
    public string? GuardianSuffix { get; set; }
    public string? GuardianEmailAddress { get; set; }
    public string? GuardianPhoneNumber { get; set; }
    public string? GuardianRelationship { get; set; }
    public bool GuardianIsAuthorizedPickup { get; set; } = true;
    public bool GuardianIsEmergencyContact { get; set; } = true;
}
