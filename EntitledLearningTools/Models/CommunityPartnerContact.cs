using System;
using System.ComponentModel.DataAnnotations;

namespace EntitledLearningTools.Models;

public record CommunityPartnerContact : PersonBase
{
    [Required]
    public string? CommunityPartnerName { get; set; }
}

