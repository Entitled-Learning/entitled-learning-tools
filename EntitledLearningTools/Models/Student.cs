﻿using System;
using System.ComponentModel.DataAnnotations;

namespace EntitledLearningTools.Models;

public class Student : TestModelBase
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
    public string? Race { get; set; }

    [Required]
    public DateTimeOffset? DateOfBirth { get; set; }

    public string? HouseholdIncomeRange { get; set; }

    public string? ShirtSize { get; set; }

    public void initWithTestData(){
        FirstName = GenerateRandomFirstName();
        LastName = GenerateRandomLastName();
        Race = "Black";
        DateOfBirth = new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero);
        ShirtSize = "M";
        City = "New York";
        State = "NY";
        ZipCode = "10001";
        AddressLine1 = "123 Main St";
    }
}

