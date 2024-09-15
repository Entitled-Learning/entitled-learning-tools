using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace EntitledLearning.Enrollment.UI.Models;

public record EntitledLearningUser
{
    public string Id { get; set; } = "";
    public string UserName { get; set; } = "";
    public string Email { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";

    public void SetClaims(IEnumerable<Claim> claims)
    {
        foreach (var claim in claims)
        {
            switch (claim.Type)
            {
                case ClaimTypes.NameIdentifier:
                    Id = claim.Value;
                    break;
                case "emails":
                    Email = claim.Value;
                    break;
                case "name":
                    UserName = claim.Value;
                    break;
                case "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname":
                    FirstName = claim.Value;
                    break;
                case "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname":
                    LastName = claim.Value;
                    break;
            }
        }
    }
}
