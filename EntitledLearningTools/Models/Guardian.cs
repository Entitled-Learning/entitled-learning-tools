using System;
using System.ComponentModel.DataAnnotations;

namespace EntitledLearningTools.Models;

public class Guardian : PersonBase
{
    [Phone]
    [Required]
    public string? CellPhoneNumber { get; set; }

    [Required]
    public string? Relationship { get; set; }

    public bool IsEmergencyContact { get; set; }

    public bool IsAuthorizedPickup { get; set; }

    public Guardian()
    {
    }
    
    public Guardian(Guardian guardian)
    {
        Prefix = guardian.Prefix;
        FirstName = guardian.FirstName;
        MiddleName = guardian.MiddleName;
        LastName = guardian.LastName;
        Suffix = guardian.Suffix;
        EmailAddress = guardian.EmailAddress;
        CellPhoneNumber = guardian.CellPhoneNumber;
        Relationship = guardian.Relationship;
        IsEmergencyContact = guardian.IsEmergencyContact;
        IsAuthorizedPickup = guardian.IsAuthorizedPickup;
        City = guardian.City;
        State = guardian.State;
        ZipCode = guardian.ZipCode;
        AddressLine1 = guardian.AddressLine1;
        AddressLine2 = guardian.AddressLine2;
    }

    public void initWithTestData(){
        FirstName = "John";
        LastName = "Doe";
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



