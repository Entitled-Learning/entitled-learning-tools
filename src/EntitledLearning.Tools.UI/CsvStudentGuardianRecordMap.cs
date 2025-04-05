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
        Map(m => m.StudentFirstName).Name("FirstName");
        Map(m => m.StudentLastName).Name("LastName");
        Map(m => m.StudentMiddleName).Name("MiddleName").Optional();
        Map(m => m.StudentPrefix).Name("Prefix").Optional();
        Map(m => m.StudentSuffix).Name("Suffix").Optional();
        Map(m => m.StudentRace).Name("Race");
        Map(m => m.StudentDateOfBirth).Name("DateOfBirth");
        Map(m => m.StudentHouseholdIncomeRange).Name("HouseholdIncomeRange");
        Map(m => m.StudentShirtSize).Name("ShirtSize");
        Map(m => m.StudentEmailAddress).Name("EmailAddress");
        Map(m => m.StudentAddressLine1).Name("AddressLine1");
        Map(m => m.StudentAddressLine2).Name("AddressLine2");
        Map(m => m.StudentCity).Name("City");
        Map(m => m.StudentState).Name("State");
        Map(m => m.StudentZipCode).Name("ZipCode");

        Map(m => m.GuardianFirstName).Name("GuardianFirstName");
        Map(m => m.GuardianLastName).Name("GuardianLastName");
        Map(m => m.GuardianEmailAddress).Name("GuardianEmail");
        Map(m => m.GuardianPhoneNumber).Name("GuardianPhoneNumber");
        Map(m => m.GuardianRelationship).Name("GuardianRelationship");
        Map(m => m.GuardianIsAuthorizedPickup).Name("GuardianIsAuthorizedPickup");
        Map(m => m.GuardianIsEmergencyContact).Name("GuardianIsEmergencyContact");
    }
}
