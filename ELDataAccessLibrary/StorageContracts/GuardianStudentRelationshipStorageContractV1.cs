// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

namespace ELDataAccessLibrary.StorageContracts;

public record GuardianStudentRelationshipStorageContractV1 : StorageContractBase
{
    public string? GuardianId { get; set; }

    public string? StudentId { get; set; }

    public string? Relationship { get; set; }

    public bool IsEmergencyContact { get; set; }

    public bool IsAuthorizedPickup { get; set; }

    public string? ContractVersion { get; } = nameof(GuardianStudentRelationshipStorageContractV1);
}

