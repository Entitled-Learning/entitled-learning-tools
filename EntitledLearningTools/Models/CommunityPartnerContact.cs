using System;
using System.ComponentModel.DataAnnotations;

namespace EntitledLearningTools.Models;

public record CommunityPartnerContact : AdultBase
{
    [Required]
    public string? CommunityPartnerName { get; set; }
}

