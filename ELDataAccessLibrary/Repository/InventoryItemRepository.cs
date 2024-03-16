// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using ELDataAccessLibrary.StorageContracts;

namespace ELDataAccessLibrary.Repository;

public class InventoryItemRepository : RepositoryBase, IDataRepository<InventoryItemStorageContractV1>
{
    private readonly ISqlDataAccess _db;
    private readonly string tableName = "InventoryItem";

    public InventoryItemRepository(ISqlDataAccess db)
    {
       _db = db;
    }

    public async Task<IEnumerable<InventoryItemStorageContractV1>> GetAllAsync()
    {
        string sql = "select * from dbo." + tableName + " order by CreatedOn;";
        var data = await _db.LoadData<InventoryItemStorageContractV1, dynamic>(sql, new { });

        return data;
    }

    public async Task<InventoryItemStorageContractV1> GetByIdAsync(string id)
    {
        string sql = "select * from dbo." + tableName + " where Id = @Id;";
        var data = await _db.LoadData<InventoryItemStorageContractV1, dynamic>(sql, new { Id = id });

        return data.FirstOrDefault()!;
    }

    public async Task<InventoryItemStorageContractV1> AddAsync(InventoryItemStorageContractV1 entity)
    {
        entity.Id = Guid.NewGuid();

        string sql = "insert into dbo." + tableName + " (Id, Name, Description, Cost, PhysicalLocation, ExpirationDate, Sku, Quantity, ContractVersion) " +
        "values (@Id, @Name, @Description, @Cost, @PhysicalLocation, @ExpirationDate, @Sku, @Quantity, @ContractVersion);";

        await _db.SaveData(sql, entity);

        return entity;
    }

    public async Task UpdateAsync(InventoryItemStorageContractV1 entity)
    {
        entity.UpdatedOn = DateTimeOffset.UtcNow;

        string sql = "update dbo." + tableName + " set Name = @Name, Description = @Description, Cost = @Cost, PhysicalLocation = @PhysicalLocation, ExpirationDate = @ExpirationDate, Sku = @Sku, Quantity = @Quantity, ContractVersion = @ContractVersion, UpdatedOn = @UpdatedOn where Id = @Id;";

        await _db.SaveData(sql, entity);
    }

    public async Task DeleteAsync(string id)
    {
        await Task.Delay(1000);
    }
}

