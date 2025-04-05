// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using EntitledLearning.Tools.UI.Models;
using EntitledLearning.Data.StorageContracts;
using Riok.Mapperly.Abstractions;
using EntitledLearning.Data.Models;

namespace EntitledLearning.Tools.UI;

[Mapper]
public partial class DataMapper
{
    public partial Student Copy(Student student);
    public partial InventoryItem Copy(InventoryItem item);
    public partial Guardian Copy(Guardian guardian);
    public partial StudentStorageContractV1 ToStudentStorageContractV1(Student student);

    [MapProperty(nameof(CsvStudentGuardianRecord.StudentPrefix), nameof(StudentStorageContractV1.Prefix))]
    [MapProperty(nameof(CsvStudentGuardianRecord.StudentFirstName), nameof(StudentStorageContractV1.FirstName))]
    [MapProperty(nameof(CsvStudentGuardianRecord.StudentMiddleName), nameof(StudentStorageContractV1.MiddleName))]
    [MapProperty(nameof(CsvStudentGuardianRecord.StudentLastName), nameof(StudentStorageContractV1.LastName))]
    [MapProperty(nameof(CsvStudentGuardianRecord.StudentSuffix), nameof(StudentStorageContractV1.Suffix))]
    [MapProperty(nameof(CsvStudentGuardianRecord.StudentEmailAddress), nameof(StudentStorageContractV1.EmailAddress))]
    [MapProperty(nameof(CsvStudentGuardianRecord.StudentAddressLine1), nameof(StudentStorageContractV1.AddressLine1))]
    [MapProperty(nameof(CsvStudentGuardianRecord.StudentAddressLine2), nameof(StudentStorageContractV1.AddressLine2))]
    [MapProperty(nameof(CsvStudentGuardianRecord.StudentCity), nameof(StudentStorageContractV1.City))]
    [MapProperty(nameof(CsvStudentGuardianRecord.StudentState), nameof(StudentStorageContractV1.State))]
    [MapProperty(nameof(CsvStudentGuardianRecord.StudentZipCode), nameof(StudentStorageContractV1.ZipCode))]
    [MapProperty(nameof(CsvStudentGuardianRecord.StudentRace), nameof(StudentStorageContractV1.Race))]
    [MapProperty(nameof(CsvStudentGuardianRecord.StudentDateOfBirth), nameof(StudentStorageContractV1.DateOfBirth))]
    [MapProperty(nameof(CsvStudentGuardianRecord.StudentHouseholdIncomeRange), nameof(StudentStorageContractV1.HouseholdIncomeRange))]
    [MapProperty(nameof(CsvStudentGuardianRecord.StudentShirtSize), nameof(StudentStorageContractV1.ShirtSize))]
    [MapProperty(nameof(CsvStudentGuardianRecord.StudentAllowPhotoRelease), nameof(StudentStorageContractV1.AllowPhotoRelease))]
    public partial StudentStorageContractV1 ToStudentStorageContractV1(CsvStudentGuardianRecord student);

    [MapProperty(nameof(CsvStudentGuardianRecord.GuardianPrefix), nameof(GuardianStorageContractV1.Prefix))]
    [MapProperty(nameof(CsvStudentGuardianRecord.GuardianFirstName), nameof(GuardianStorageContractV1.FirstName))]
    [MapProperty(nameof(CsvStudentGuardianRecord.GuardianMiddleName), nameof(GuardianStorageContractV1.MiddleName))]
    [MapProperty(nameof(CsvStudentGuardianRecord.GuardianLastName), nameof(GuardianStorageContractV1.LastName))]
    [MapProperty(nameof(CsvStudentGuardianRecord.GuardianEmailAddress), nameof(GuardianStorageContractV1.EmailAddress))]
    [MapProperty(nameof(CsvStudentGuardianRecord.GuardianPhoneNumber), nameof(GuardianStorageContractV1.CellPhoneNumber))]
    [MapProperty(nameof(CsvStudentGuardianRecord.GuardianPrefix), nameof(GuardianStorageContractV1.Prefix))]
    public partial GuardianStorageContractV1 ToGuardianStorageContractV1(CsvStudentGuardianRecord guardian);

    public partial Guardian ToGuardian(StudentGuardian guardian);
    public partial Student ToStudent(StudentStorageContractV1 student);
    public partial InventoryItem ToInventoryItem(InventoryItemStorageContractV1 item);
    public partial GuardianStorageContractV1 ToGuardianStorageContractV1(Guardian guardian);
    public partial CommunityPartnerStorageContractV1 ToCommunityPartnerStorageContractV1(CommunityPartner communityPartner);
    public partial CommunityPartnerContactStorageContractV1 ToCommunityPartnerContactStorageContractV1(CommunityPartnerContact communityPartnerContact);
    public partial CommunityPartnerStorageContractV1 ToCommunityPartnerStorageContractV1(CsvCommunityPartnerRecord communityPartner);
    public partial InventoryItemStorageContractV1 ToInventoryItemStorageContractV1(InventoryItem item);
}