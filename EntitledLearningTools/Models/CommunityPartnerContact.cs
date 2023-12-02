using System;
using System.ComponentModel.DataAnnotations;

namespace EntitledLearningTools.Models;

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
}

