// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using Microsoft.Extensions.Logging;
using EntitledLearning.Data.SqlClient;
using EntitledLearning.Data.StorageContracts;

namespace EntitledLearning.Data.Repository;

public class InventoryItemRepository : RepositoryBase, IDataRepository<InventoryItemStorageContractV1>
{
    private readonly string tableName = "InventoryItem";
    private readonly ISqlDataClient _db;
    private readonly ILogger<InventoryItemRepository> _logger;

    public InventoryItemRepository(ISqlDataClient db, ILogger<InventoryItemRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<IEnumerable<InventoryItemStorageContractV1>> GetAllAsync()
    {
        string sql = "select * from dbo." + tableName + " order by CreatedOn;";

        try{
            var data = await _db.LoadData<InventoryItemStorageContractV1, dynamic>(sql, new { });
            return data;
        } catch (Exception ex) {
            _logger.GetInventoryError(ex);
            throw;
        }
    }

    public async Task<InventoryItemStorageContractV1> GetByIdAsync(string id)
    {
        string sql = "select * from dbo." + tableName + " where Id = @Id;";

        try{
            var data = await _db.LoadData<InventoryItemStorageContractV1, dynamic>(sql, new { Id = id });
            return data.FirstOrDefault()!;
        } catch (Exception ex) {
            _logger.GetInventoryError(ex);
            throw;
        }
    }

    public async Task<InventoryItemStorageContractV1> AddAsync(InventoryItemStorageContractV1 entity)
    {
        entity.Id = Guid.NewGuid();

        string sql = "insert into dbo." + tableName + " (Id, Name, Description, Cost, PhysicalLocation, ExpirationDate, Sku, Quantity, ContractVersion) " +
        "values (@Id, @Name, @Description, @Cost, @PhysicalLocation, @ExpirationDate, @Sku, @Quantity, @ContractVersion);";

        try{
            await _db.SaveData(sql, entity);
            return entity;
        } catch (Exception ex) {
            _logger.CreateInventoryError(ex);
            throw;
        }
    }

    public async Task UpdateAsync(InventoryItemStorageContractV1 entity)
    {
        entity.UpdatedOn = DateTimeOffset.UtcNow;

        string sql = "update dbo." + tableName + " set Name = @Name, Description = @Description, Cost = @Cost, PhysicalLocation = @PhysicalLocation, ExpirationDate = @ExpirationDate, Sku = @Sku, Quantity = @Quantity, ContractVersion = @ContractVersion, UpdatedOn = @UpdatedOn where Id = @Id;";

        try{
            await _db.SaveData(sql, entity);
        } catch (Exception ex) {
            _logger.UpdateInventoryError(ex);
            throw;
        }
    }

    public async Task DeleteAsync(string id)
    {   
        try{
            string sql = "delete from dbo." + tableName + " where Id = @Id;";
            await _db.SaveData(sql, new { Id = id });
        } catch (Exception ex) {
            _logger.DeleteInventoryError(ex);
            throw;
        }
    }
}

