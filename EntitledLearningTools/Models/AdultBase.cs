using System;
using System.ComponentModel.DataAnnotations;

namespace EntitledLearningTools.Models;

public abstract record AdultBase : PersonBase
{
    [ValidPhoneNumberOrEmpty]
    public string? OfficePhoneNumber { get; set; }

    [Phone]
    [Required]
    public string? CellPhoneNumber { get; set; }

}

