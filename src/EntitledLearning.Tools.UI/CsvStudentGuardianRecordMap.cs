// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using CsvHelper.Configuration;
using EntitledLearning.Tools.UI.Models;

namespace EntitledLearning.Tools.UI;

public sealed class CsvStudentGuardianRecordMap : ClassMap<CsvStudentGuardianRecord>
{
    public CsvStudentGuardianRecordMap()
    {
        // Student fields
        Map(m => m.StudentFirstName).Name("FirstName");
        Map(m => m.StudentLastName).Name("LastName");
        Map(m => m.StudentMiddleName).Name("MiddleName").Optional();
        Map(m => m.StudentPrefix).Name("Prefix").Optional();
        Map(m => m.StudentSuffix).Name("Suffix").Optional();
        
        // Make previously required fields optional
        Map(m => m.StudentRace).Name("Race").Optional();
        Map(m => m.StudentDateOfBirth).Name("DateOfBirth").Optional();
        Map(m => m.StudentHouseholdIncomeRange).Name("HouseholdIncomeRange").Optional();
        Map(m => m.StudentShirtSize).Name("ShirtSize").Optional();
        
        // Map Email to EmailAddress since the CSV uses Email
        Map(m => m.StudentEmailAddress).Name("Email").Optional();
        
        Map(m => m.StudentAddressLine1).Name("AddressLine1").Optional();
        Map(m => m.StudentAddressLine2).Name("AddressLine2").Optional();
        Map(m => m.StudentCity).Name("City").Optional();
        Map(m => m.StudentState).Name("State").Optional();
        Map(m => m.StudentZipCode).Name("ZipCode").Optional();

        // Guardian fields
        Map(m => m.GuardianFirstName).Name("GuardianFirstName").Optional();
        Map(m => m.GuardianLastName).Name("GuardianLastName").Optional();
        Map(m => m.GuardianEmailAddress).Name("GuardianEmail").Optional();
        
        // Make previously required fields optional
        Map(m => m.GuardianPhoneNumber).Name("GuardianPhoneNumber").Optional();
        Map(m => m.GuardianRelationship).Name("GuardianRelationship").Optional();
        Map(m => m.GuardianIsAuthorizedPickup).Name("GuardianIsAuthorizedPickup").Optional();
        Map(m => m.GuardianIsEmergencyContact).Name("GuardianIsEmergencyContact").Optional();
    }
}
