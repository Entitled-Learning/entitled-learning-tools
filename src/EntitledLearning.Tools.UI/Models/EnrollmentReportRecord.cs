// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

namespace EntitledLearning.Tools.UI.Models;

public record EnrollmentReportRecord
{
    // Scholar Properties
    public string? ScholarId { get; set; }
    public string? ScholarName { get; set; }
    public string? Age { get; set; }
    public string? Birthday { get; set; }
    public string? Allergies { get; set; }
    public string? School { get; set; }
    public string? MedicationInstructions { get; set; }
    public string? Gender { get; set; }
    public string? Race { get; set; }
    public string? GradeLevel { get; set; }
    public string? LearningAccommodations { get; set; }
    public string? SummerElective { get; set; }
    public string? CareerInterests { get; set; }
    public string? ShirtSize { get; set; }
    public string? AllowPhotoRelease { get; set; }
    
    // Parent/Guardian Properties
    public string? ParentName { get; set; }
    public string? ParentEmail { get; set; }
    public string? ParentPhone { get; set; }
    public string? ParentAddressStreet1 { get; set; }
    public string? ParentAddressStreet2 { get; set; }
    public string? ParentCity { get; set; }
    public string? ParentState { get; set; }
    public string? ParentZipcode { get; set; }

    // Secondary Contact
    public string? CaregiverName { get; set; }
    public string? CaregiverPhone { get; set; }
    public string? SecondaryContactName { get; set; }
    public string? SecondaryContactEmail { get; set; }
    public string? SecondaryContactPhone { get; set; }
    
    // Additional Information
    public string? AuthorizedPickupName { get; set; }
    public string? ContactNumber { get; set; }
    public string? HowDidYouHearAboutUs { get; set; }
    public string? ConsentToTravel { get; set; }
    public string? InterestedInChaperone { get; set; }
    public string? WishToReceiveEmails { get; set; }
}