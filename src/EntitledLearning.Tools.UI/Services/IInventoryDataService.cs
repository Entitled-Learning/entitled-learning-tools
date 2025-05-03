using EntitledLearning.Data.Models;
using EntitledLearning.Data.StorageContracts;
using EntitledLearning.Tools.UI.Models;

namespace EntitledLearning.Tools.UI.Services;

public interface IInventoryDataService
{
    Task<IEnumerable<InventoryItemStorageContractV1>> GetAllItemsAsync();
    Task<IEnumerable<InventoryItemStorageContractV1>> FilterItemsAsync(string searchTerm, IEnumerable<InventoryItemStorageContractV1> items);
    Task<bool> UpdateItemAsync(InventoryItem item);
    Task<bool> DeleteItemAsync(string id);
    Task<MemoryStream> GenerateExcelFileAsync(IEnumerable<InventoryItemStorageContractV1> items);
}