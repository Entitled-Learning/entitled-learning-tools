// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace EntitledLearning.Enrollment.UI.Models;

public class CommunityPartnerContact
{
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

    [Required]
    public string? CommunityPartnerName { get; set; }

    [ValidPhoneNumberOrEmpty]
    public string? OfficePhoneNumber { get; set; }

    [Phone]
    [Required]
    public string? CellPhoneNumber { get; set; }

    public void initWithTestData(){
        FirstName = "Olumide";
        LastName = "Daramola";
        EmailAddress = "tope.daram@gmail.com";
        City = "Dallas";
        State = "Texas";
        ZipCode = "10001";
        AddressLine1 = "123 Main St";
        CellPhoneNumber = "1234567890";
    }
}

