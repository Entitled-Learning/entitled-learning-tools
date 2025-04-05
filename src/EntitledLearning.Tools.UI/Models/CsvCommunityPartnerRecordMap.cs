using CsvHelper.Configuration;

namespace EntitledLearning.Tools.UI.Models;

public class CsvCommunityPartnerRecordMap : ClassMap<CsvCommunityPartnerRecord>
{
    public CsvCommunityPartnerRecordMap()
    {
        Map(m => m.Name).Name("Name");
        Map(m => m.PhoneNumber).Name("PhoneNumber");
        Map(m => m.EmailAddress).Name("EmailAddress");
        Map(m => m.AddressLine1).Name("AddressLine1");
        Map(m => m.AddressLine2).Name("AddressLine2");
        Map(m => m.City).Name("City");
        Map(m => m.State).Name("State");
        Map(m => m.ZipCode).Name("ZipCode");
    }
}