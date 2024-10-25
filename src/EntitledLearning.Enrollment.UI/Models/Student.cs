// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace EntitledLearning.Enrollment.UI.Models;

public class Student : TestModelBase
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

    [Required]
    public string? Race { get; set; }

    [Required]
    public DateTimeOffset? DateOfBirth { get; set; }

    public string? HouseholdIncomeRange { get; set; }

    public string? ShirtSize { get; set; }

    public bool IsScholar { get; set; }
    
    public bool AllowPhotoRelease { get; set; }

    public Student(bool initTestData = false)
    {
        if (initTestData)
        {
            initWithTestData();
        }
    }

    public void initWithTestData(){
        FirstName = "Kenia";
        LastName = "Kemp";
        Race = "Black";
        DateOfBirth = new DateTimeOffset(2009, 7, 23, 0, 0, 0, TimeSpan.Zero);
        ShirtSize = "L";
        City = "Jackson";
        State = "Mississippi";
        ZipCode = "75126";
        AddressLine1 = "7601 E. 47th Ter.";
    }
}

