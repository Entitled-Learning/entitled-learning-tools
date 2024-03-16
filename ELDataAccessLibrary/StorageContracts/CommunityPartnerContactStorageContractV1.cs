// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

namespace ELDataAccessLibrary.StorageContracts;

public record CommunityPartnerContactStorageContractV1 : StorageContractBase
{
    public string? Id { get; set; }

    public string? Prefix { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }
    
    public string? LastName { get; set; }

    public string? Suffix { get; set; }

    public string? OfficePhoneNumber { get; set; }
    
    public string? CellPhoneNumber { get; set; }
    
    public string? EmailAddress { get; set; }

    public string? AddressLine1 { get; set; }
    
    public string? AddressLine2 { get; set; }
    
    public string? City { get; set; }
    
    public string? State { get; set; }
    
    public string? ZipCode { get; set; }

    public string? CommunityPartnerName { get; set; }

    public string? ContractVersion { get; } = nameof(CommunityPartnerContactStorageContractV1);
}

