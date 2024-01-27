using System;
using System.ComponentModel.DataAnnotations;

namespace EntitledLearningTools.Models;

public class Guardian : TestModelBase
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
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

    public void initWithTestData(){
        FirstName = GenerateRandomFirstName();
        LastName = GenerateRandomLastName();
        EmailAddress = "john.doe@gmail.com";
        CellPhoneNumber = "123-456-7890";
        Relationship = "Father";
        IsEmergencyContact = true;
        IsAuthorizedPickup = true;
        City = "New York";
        State = "NY";
        ZipCode = "10001";
        AddressLine1 = "123 Main St";
    }
}



