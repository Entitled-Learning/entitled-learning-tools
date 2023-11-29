using System;
using System.ComponentModel.DataAnnotations;

namespace EntitledLearningTools.Models;

public record Guardian : AdultBase
{
    [Required]
    public string? Relationship { get; set; }

    public bool IsEmergencyContact { get; set; }

    public bool IsAuthorizedPickup { get; set; }
}



