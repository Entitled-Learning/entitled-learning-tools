// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using CsvHelper.Configuration;
using EntitledLearning.Tools.UI.Models;

namespace EntitledLearning.Tools.UI;

public sealed class EnrollmentReportRecordMap : ClassMap<EnrollmentReportRecord>
{
    public EnrollmentReportRecordMap()
    {
        // Scholar Properties
        Map(m => m.ScholarId).Name("Scholar ID").Optional();
        Map(m => m.ScholarName).Name("Scholar Name").Optional();
        Map(m => m.Age).Name("Age").Optional();
        Map(m => m.Birthday).Name("Birthday").Optional();
        Map(m => m.Allergies).Name("Allergies").Optional();
        Map(m => m.School).Name("School").Optional();
        Map(m => m.MedicationInstructions).Name("In the event that your scholar needs to take medication/s (inhaler, allergy pill, etc.) please provide the name of the medication(s) and brief instructions on how and when to take them.").Optional();
        Map(m => m.Gender).Name("Gender").Optional();
        Map(m => m.Race).Name("Race").Optional();
        Map(m => m.GradeLevel).Name("The grade level just completed").Optional();
        Map(m => m.LearningAccommodations).Name("If your scholar needs learning accomodations, briefly describe these accomodations.").Optional();
        Map(m => m.SummerElective).Name("Choose a summer elective that may interest your scholar.").Optional();
        Map(m => m.CareerInterests).Name("Choose two top career industries your scholar may be interested in pursuing this summer. Students will complete a digital interview video prior to the start date.").Optional();
        Map(m => m.ShirtSize).Name("Shirt Size").Optional();
        Map(m => m.AllowPhotoRelease).Name("We often take photos of class and post them to our social media, is it okay to post pictures of your scholar?").Optional();
        
        // Parent/Guardian Properties
        Map(m => m.ParentName).Name("Parent Name").Optional();
        Map(m => m.ParentEmail).Name("Parent Email").Optional();
        Map(m => m.ParentPhone).Name("Parent Phone").Optional();
        Map(m => m.ParentAddressStreet1).Name("Parent Address Street 1").Optional();
        Map(m => m.ParentAddressStreet2).Name("Parent Address Street 2").Optional();
        Map(m => m.ParentCity).Name("Parent City").Optional();
        Map(m => m.ParentState).Name("Parent State").Optional();
        Map(m => m.ParentZipcode).Name("Parent Zipcode").Optional();
        
        // Secondary Contact
        Map(m => m.CaregiverName).Name("Caregiver Name").Optional();
        Map(m => m.CaregiverPhone).Name("Caregiver Phone").Optional();
        Map(m => m.SecondaryContactName).Name("Secondary Contact Name").Optional();
        Map(m => m.SecondaryContactEmail).Name("Secondary Contact Email").Optional();
        Map(m => m.SecondaryContactPhone).Name("Secondary Contact Phone").Optional();
        
        // Additional Information
        Map(m => m.AuthorizedPickupName).Name("authorized for pick-up Name").Optional();
        Map(m => m.ContactNumber).Name("Contact Number").Optional();
        Map(m => m.HowDidYouHearAboutUs).Name("How did you hear about us?").Optional();
        Map(m => m.ConsentToTravel).Name("During our 7-week program, we plan to travel for field trips. Do you consent to have your scholar/s travel by bus to these locations: Biz Town, Blue River Golf Course, George Washington Carver Museum. (Dates: TBA)").Optional();
        Map(m => m.InterestedInChaperone).Name("Are you interested in joining us as a chaperone? (Please note that chaperones will be responsible for paying all trip fees).").Optional();
        Map(m => m.WishToReceiveEmails).Name("I wish to receive emails containing news and updates.").Optional();
    }
}