using System;

namespace ELDataAccessLibrary.StorageContracts;

public record StudentStorageContractV1 : StorageContractBase
{
    public string? Id { get; set; }

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

    public string? Race { get; set; }

    public DateTimeOffset? DateOfBirth { get; set; }

    public string? HouseholdIncomeRange { get; set; }

    public string? ShirtSize { get; set; }

    public string? ContractVersion { get; } = nameof(StudentStorageContractV1);
}

