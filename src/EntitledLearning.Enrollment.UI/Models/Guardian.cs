// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace EntitledLearning.Enrollment.UI.Models;

public class Guardian : TestModelBase
{
    public string? Id { get; set; }
    
    public string? Prefix { get; set; }

    [Required]
    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    [Required]
    public string? LastName { get; set; }

    public string? Suffix { get; set; }

    [ValidEmailOrEmpty]
    public string? EmailAddress { get; set; }

    [Required]
    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    [Required]
    public string? City { get; set; }

    [Required]
    public string? State { get; set; }

    [Required]
    [RegularExpression(@"^\d{5}(?:[-\s]\d{4})?$", ErrorMessage = "Invalid Zip Code")]
    public string? ZipCode { get; set; }

    [Phone]
    [Required]
    public string? CellPhoneNumber { get; set; }

    [Required]
    public string? Relationship { get; set; }

    public bool IsEmergencyContact { get; set; }

    public bool IsAuthorizedPickup { get; set; }

    public bool ReceiveUpdates { get; set; }

    public void initWithTestData(){
        FirstName = GenerateRandomFirstName();
        LastName = GenerateRandomLastName();
        EmailAddress = "john.doe@gmail.com";
        CellPhoneNumber = "123-456-7890";
        Relationship = "Father";
        IsEmergencyContact = true;
        IsAuthorizedPickup = true;
        City = "Jackson";
        State = "Mississippi";
        ZipCode = "75126";
        AddressLine1 = "123 Main St";
    }
}



