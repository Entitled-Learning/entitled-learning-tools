// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

namespace EntitledLearning.Data.StorageContracts;

public record AcademicTermStorageContractV1 : StorageContractBase
{
    public string? TermId { get; set; }
    public string? TermName { get; set; }
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
    public bool IsActive { get; set; }
    public string? ContractVersion { get; } = nameof(AcademicTermStorageContractV1);
}
