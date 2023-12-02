using System;

namespace ELDataAccessLibrary.StorageContracts;

public record GuardianStorageContractV1 : ContractBase
{
    public string Id { get => GenerateId(FirstName, LastName); }
    
    public string? Prefix { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? Suffix { get; set; }

    public string? EmailAddress { get; set; }

    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? ZipCode { get; set; }


    public string? CellPhoneNumber { get; set; }

    public string? Relationship { get; set; }

    public bool IsEmergencyContact { get; set; }

    public bool IsAuthorizedPickup { get; set; }

    public string? ContractVersion { get; } = "GuardianStorageContractV1";
}