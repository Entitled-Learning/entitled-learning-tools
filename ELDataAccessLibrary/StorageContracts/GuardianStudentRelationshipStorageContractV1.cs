using System;

namespace ELDataAccessLibrary.StorageContracts;

public record GuardianStudentRelationshipStorageContractV1 : ContractBase
{
    public string? GuardianId { get; set; }

    public string? StudentId { get; set; }

    public string? ContractVersion { get; } = "GuardianStudentRelationshipStorageContractV1";
}

