using System;
using System.ComponentModel.DataAnnotations;

namespace EntitledLearningTools.Models;

public class Student : PersonBase
{
    [Required]
    public string? Race { get; set; }

    [Required]
    public DateTimeOffset? DateOfBirth { get; set; }

    public string? HouseholdIncomeRange { get; set; }

    public string? ShirtSize { get; set; }
}

