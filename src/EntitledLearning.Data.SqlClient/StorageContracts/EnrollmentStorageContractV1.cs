// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

namespace EntitledLearning.Data.StorageContracts;

public record EnrollmentStorageContractV1 : StorageContractBase
{
    public string? EnrollmentId { get; set; }
    public string? StudentId { get; set; }
    public string? TermId { get; set; }
    public DateTimeOffset? EnrollmentDate { get; set; }
    public string? EnrollmentStatus { get; set; }
    public string? Notes { get; set; }
    public string? ContractVersion { get; } = nameof(EnrollmentStorageContractV1);
}
