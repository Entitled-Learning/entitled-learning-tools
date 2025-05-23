// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using EntitledLearning.Tools.UI.Models;
using EntitledLearning.Data.StorageContracts;
using Riok.Mapperly.Abstractions;
using EntitledLearning.Data.Models;
using EntitledLearning.Tools.UI.Utilities;
using System.Globalization;

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
    
    // New methods for handling enrollment report format
    public StudentStorageContractV1 ToStudentStorageContractV1(EnrollmentReportRecord record)
    {
        var studentContract = new StudentStorageContractV1();
        
        // Parse scholar name into components
        var (prefix, firstName, middleName, lastName, suffix) = NameParser.ParseFullName(record.ScholarName);
        
        // Basic student information
        studentContract.Id = record.ScholarId;
        studentContract.Prefix = prefix;
        studentContract.FirstName = firstName;
        studentContract.MiddleName = middleName;
        studentContract.LastName = lastName;
        studentContract.Suffix = suffix;
        
        // Parse and set date of birth if available
        if (DateTime.TryParse(record.Birthday, out DateTime birthDate))
        {
            studentContract.DateOfBirth = birthDate;
        }
        
        // Demographics
        studentContract.Race = record.Race;
        studentContract.ShirtSize = record.ShirtSize;
        studentContract.Gender = record.Gender;
        
        // School information
        studentContract.School = record.School;
        studentContract.GradeLevel = record.GradeLevel;
        
        // Medical and accommodations
        studentContract.Allergies = record.Allergies;
        studentContract.MedicationInstructions = record.MedicationInstructions;
        studentContract.LearningAccommodations = record.LearningAccommodations;
        
        // Contact info - use parent address for student
        studentContract.AddressLine1 = record.ParentAddressStreet1;
        studentContract.AddressLine2 = record.ParentAddressStreet2;
        studentContract.City = record.ParentCity;
        studentContract.State = record.ParentState;
        studentContract.ZipCode = record.ParentZipcode;
        
        // Program information
        studentContract.SummerElective = record.SummerElective;
        studentContract.CareerInterests = record.CareerInterests;
        studentContract.AllowPhotoRelease = record.AllowPhotoRelease != null && record.AllowPhotoRelease.ToLowerInvariant() == "yes";
        
        return studentContract;
    }
    
    public GuardianStorageContractV1 ToPrimaryGuardianStorageContractV1(EnrollmentReportRecord record)
    {
        var guardianContract = new GuardianStorageContractV1();
        
        // Parse parent name into components
        var (prefix, firstName, middleName, lastName, suffix) = NameParser.ParseFullName(record.ParentName);
        
        // Basic guardian information
        guardianContract.Prefix = prefix;
        guardianContract.FirstName = firstName;
        guardianContract.MiddleName = middleName;
        guardianContract.LastName = lastName;
        guardianContract.Suffix = suffix;
        
        // Contact information
        guardianContract.EmailAddress = record.ParentEmail;
        guardianContract.CellPhoneNumber = record.ParentPhone;
        
        // Address
        guardianContract.AddressLine1 = record.ParentAddressStreet1;
        guardianContract.AddressLine2 = record.ParentAddressStreet2;
        guardianContract.City = record.ParentCity;
        guardianContract.State = record.ParentState;
        guardianContract.ZipCode = record.ParentZipcode;
        
        // Additional guardian properties
        if (!string.IsNullOrEmpty(record.WishToReceiveEmails))
        {
            guardianContract.ReceiveUpdates = record.WishToReceiveEmails.Trim().Equals("Yes", StringComparison.OrdinalIgnoreCase);
        }
        
        // Set relationship
        guardianContract.Relationship = "Parent/Guardian";
        guardianContract.IsAuthorizedPickup = true;
        guardianContract.IsEmergencyContact = true;
        
        return guardianContract;
    }
    
    // Create a guardian record for the caregiver
    public GuardianStorageContractV1? ToCaregiverGuardianStorageContractV1(EnrollmentReportRecord record)
    {
        if (string.IsNullOrWhiteSpace(record.CaregiverName))
            return null;
            
        var guardianContract = new GuardianStorageContractV1();
        
        // Parse caregiver name into components
        var (prefix, firstName, middleName, lastName, suffix) = NameParser.ParseFullName(record.CaregiverName);
        
        // Basic guardian information
        guardianContract.Prefix = prefix;
        guardianContract.FirstName = firstName;
        guardianContract.MiddleName = middleName;
        guardianContract.LastName = lastName;
        guardianContract.Suffix = suffix;
        
        // Contact information
        guardianContract.CellPhoneNumber = record.CaregiverPhone;
        
        // Address - use same address as primary guardian if available
        guardianContract.AddressLine1 = record.ParentAddressStreet1;
        guardianContract.AddressLine2 = record.ParentAddressStreet2;
        guardianContract.City = record.ParentCity;
        guardianContract.State = record.ParentState;
        guardianContract.ZipCode = record.ParentZipcode;
        
        // Set relationship
        guardianContract.Relationship = "Caregiver";
        guardianContract.IsAuthorizedPickup = true;
        guardianContract.IsEmergencyContact = true;
        
        return guardianContract;
    }
    
    // Create a guardian record for the secondary contact
    public GuardianStorageContractV1? ToSecondaryContactGuardianStorageContractV1(EnrollmentReportRecord record)
    {
        if (string.IsNullOrWhiteSpace(record.SecondaryContactName))
            return null;
            
        var guardianContract = new GuardianStorageContractV1();
        
        // Parse secondary contact name into components
        var (prefix, firstName, middleName, lastName, suffix) = NameParser.ParseFullName(record.SecondaryContactName);
        
        // Basic guardian information
        guardianContract.Prefix = prefix;
        guardianContract.FirstName = firstName;
        guardianContract.MiddleName = middleName;
        guardianContract.LastName = lastName;
        guardianContract.Suffix = suffix;
        
        // Contact information
        guardianContract.EmailAddress = record.SecondaryContactEmail;
        guardianContract.CellPhoneNumber = record.SecondaryContactPhone;
        
        // Set relationship
        guardianContract.Relationship = "Secondary Contact";
        guardianContract.IsAuthorizedPickup = true;
        guardianContract.IsEmergencyContact = true;
        
        return guardianContract;
    }
    
    // Create a guardian record for the authorized pickup
    public GuardianStorageContractV1? ToAuthorizedPickupGuardianStorageContractV1(EnrollmentReportRecord record)
    {
        if (string.IsNullOrWhiteSpace(record.AuthorizedPickupName))
            return null;
            
        var guardianContract = new GuardianStorageContractV1();
        
        // Parse authorized pickup name into components
        var (prefix, firstName, middleName, lastName, suffix) = NameParser.ParseFullName(record.AuthorizedPickupName);
        
        // Basic guardian information
        guardianContract.Prefix = prefix;
        guardianContract.FirstName = firstName;
        guardianContract.MiddleName = middleName;
        guardianContract.LastName = lastName;
        guardianContract.Suffix = suffix;
        
        // Contact information
        guardianContract.CellPhoneNumber = record.ContactNumber;
        
        // Set relationship
        guardianContract.Relationship = "Authorized for Pickup";
        guardianContract.IsAuthorizedPickup = true;
        guardianContract.IsEmergencyContact = false;
        
        return guardianContract;
    }
    
    // For backward compatibility
    public GuardianStorageContractV1 ToGuardianStorageContractV1(EnrollmentReportRecord record)
    {
        return ToPrimaryGuardianStorageContractV1(record);
    }
    
    public StudentStorageContractV1 ProcessEnrollmentRecord(EnrollmentReportRecord record)
    {
        // Convert the record to a student storage contract
        var studentContract = ToStudentStorageContractV1(record);
        return studentContract;
    }
    
    public GuardianStorageContractV1 ProcessGuardianFromEnrollmentRecord(EnrollmentReportRecord record)
    {
        // Convert the record to a guardian storage contract
        var guardianContract = ToPrimaryGuardianStorageContractV1(record);
        return guardianContract;
    }
}