using EntitledLearning.Data.Models;
using EntitledLearning.Data.Repository;
using EntitledLearning.Data.StorageContracts;
using EntitledLearning.Tools.UI.Models;
using OfficeOpenXml;

namespace EntitledLearning.Tools.UI.Services;

public class InventoryDataService : IInventoryDataService
{
    private readonly InventoryItemRepository _inventoryRepository;
    private readonly DataMapper _mapper;
    private readonly ILogger<InventoryDataService> _logger;

    public InventoryDataService(
        InventoryItemRepository inventoryRepository,
        DataMapper mapper,
        ILogger<InventoryDataService> logger)
    {
        _inventoryRepository = inventoryRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<InventoryItemStorageContractV1>> GetAllItemsAsync()
    {
        try
        {
            return await _inventoryRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving inventory items");
            throw;
        }
    }

    public Task<IEnumerable<InventoryItemStorageContractV1>> FilterItemsAsync(string searchTerm, IEnumerable<InventoryItemStorageContractV1> items)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return Task.FromResult(items);

        searchTerm = searchTerm.Trim();
        
        var filteredItems = items.Where(t =>
            (t.Name != null && t.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
            (t.Description != null && t.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
            (t.PhysicalLocation != null && t.PhysicalLocation.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
            (t.Sku != null && t.Sku.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)));

        return Task.FromResult(filteredItems);
    }

    public async Task<bool> UpdateItemAsync(InventoryItem item)
    {
        try
        {
            var storageContract = _mapper.ToInventoryItemStorageContractV1(item);
            await _inventoryRepository.UpdateAsync(storageContract);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating inventory item");
            return false;
        }
    }

    public async Task<bool> DeleteItemAsync(string id)
    {
        try
        {
            await _inventoryRepository.DeleteAsync(id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting inventory item");
            return false;
        }
    }

    public async Task<MemoryStream> GenerateExcelFileAsync(IEnumerable<InventoryItemStorageContractV1> items)
    {
        try
        {
            var itemData = items.Select(x => x.ToDictionary()).ToList();
            
            var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Inventory");

            // Add headers
            if (itemData.Count > 0)
            {
                int column = 1;
                foreach (var header in itemData.First().Keys)
                {
                    worksheet.Cells[1, column].Value = header;
                    column++;
                }

                // Add data
                int row = 2;
                foreach (var dataRow in itemData)
                {
                    column = 1;
                    foreach (var value in dataRow.Values)
                    {
                        worksheet.Cells[row, column].Value = value;
                        column++;
                    }
                    row++;
                }
            }

            var memoryStream = new MemoryStream();
            await package.SaveAsAsync(memoryStream);
            memoryStream.Position = 0;
            
            return memoryStream;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating Excel file");
            throw;
        }
    }
}