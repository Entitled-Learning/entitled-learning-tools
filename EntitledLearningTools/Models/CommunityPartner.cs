using System;
using System.ComponentModel.DataAnnotations;

namespace EntitledLearningTools.Models;

public record CommunityPartner
{
    [Required]
    public string? Name { get; set; }

    [Required]
    [Phone]
    public string? PhoneNumber { get; set; }

    [Required]
    [EmailAddress]
    public string? EmailAddress { get; set; }

    [Required]
    public string? AddressLine1 { get; set; }

    [Required]
    public string? AddressLine2 { get; set; }

    [Required]
    public string? City { get; set; }

    [Required]
    public string? State { get; set; }

    [Required]
    [RegularExpression(@"^\d{5}(?:[-\s]\d{4})?$", ErrorMessage = "Invalid Zip Code")]
    public string? ZipCode { get; set; }
}
