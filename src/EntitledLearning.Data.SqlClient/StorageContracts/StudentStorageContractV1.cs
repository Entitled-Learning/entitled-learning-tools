// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

namespace EntitledLearning.Data.StorageContracts;

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
    public bool IsScholar { get; set; }
    public bool AllowPhotoRelease { get; set; }
    public string? ContractVersion { get; } = nameof(StudentStorageContractV1);
    public int Age => CalculateAge();

    private int CalculateAge()
    {
        if (DateOfBirth.HasValue)
        {
            DateTimeOffset today = DateTimeOffset.UtcNow;
            int age = today.Year - DateOfBirth.Value.Year;
            
            // Adjust age if the birthday hasn't occurred yet this year
            if (DateOfBirth.Value > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }

        return 0; // or throw an exception, depending on your requirements
    }
}
