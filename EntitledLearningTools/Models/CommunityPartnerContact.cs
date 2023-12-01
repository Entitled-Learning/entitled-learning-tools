using System;
using System.ComponentModel.DataAnnotations;

namespace EntitledLearningTools.Models;

public class CommunityPartnerContact: PersonBase
{
    [Required]
    public string? CommunityPartnerName { get; set; }

    [ValidPhoneNumberOrEmpty]
    public string? OfficePhoneNumber { get; set; }

    [Phone]
    [Required]
    public string? CellPhoneNumber { get; set; }
}

