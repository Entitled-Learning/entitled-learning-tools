using System;
using System.ComponentModel.DataAnnotations;

namespace EntitledLearningTools.Models;

public abstract record PersonBase
{
    public int Id { get; set; }

    public string? Prefix { get; set; }

    [Required]
    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    [Required]
    public string? LastName { get; set; }

    public string? Suffix { get; set; }

    [Phone]
    public string? OfficePhoneNumber { get; set; }

    [Required]
    [Phone]
    public string? CellPhoneNumber { get; set; }

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

