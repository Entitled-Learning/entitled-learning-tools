using EntitledLearning.Data.StorageContracts;
using Microsoft.AspNetCore.Components.Forms;

namespace EntitledLearning.Tools.UI.Services;

public interface ICommunityPartnerDataService
{
    Task<IEnumerable<CommunityPartnerStorageContractV1>> GetAllPartnersAsync();
    Task<IEnumerable<CommunityPartnerStorageContractV1>> FilterPartnersAsync(string searchTerm, IEnumerable<CommunityPartnerStorageContractV1> partners);
    Task<MemoryStream> GenerateExcelFileAsync(IEnumerable<CommunityPartnerStorageContractV1> partners);
    Task<bool> ProcessCsvFileAsync(IBrowserFile file);
    bool ValidateCsvFile(IBrowserFile file);
    Task<string> GetSampleCsvContentAsync();
}