// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using ELDataAccessLibrary.StorageContracts;

namespace ELDataAccessLibrary.Repository;

public class GuardianRepository : RepositoryBase, IDataRepository<GuardianStorageContractV1>
{
    private readonly ISqlDataAccess _db;
    private readonly string tableName = "Guardian";

    public GuardianRepository(ISqlDataAccess db)
    {
       _db = db;
    }

    public async Task<IEnumerable<GuardianStorageContractV1>> GetAllAsync()
    {
        string sql = "select * from dbo." + tableName + ";";
        var data = await _db.LoadData<GuardianStorageContractV1, dynamic>(sql, new { });

        return data;
    }

    public async Task<GuardianStorageContractV1> GetByIdAsync(string id)
    {
        string sql = "select * from dbo." + tableName + " where Id = @Id;";
        var data = await _db.LoadData<GuardianStorageContractV1, dynamic>(sql, new { Id = id });

        return data.FirstOrDefault()!;
    }

    public async Task<GuardianStorageContractV1> AddAsync(GuardianStorageContractV1 entity)
    {
        entity.Id = GenerateId(entity.FirstName, entity.LastName);

        string sql = "insert into dbo." + tableName + " (Id, Prefix, FirstName, MiddleName, LastName, Suffix, CellPhoneNumber, EmailAddress, AddressLine1, AddressLine2, City, State, ZipCode, ReceiveUpdates, ContractVersion) " +
        "values (@Id, @Prefix, @FirstName, @MiddleName, @LastName, @Suffix, @CellPhoneNumber, @EmailAddress, @AddressLine1, @AddressLine2, @City, @State, @ZipCode, @ReceiveUpdates, @ContractVersion);";

        await _db.SaveData(sql, entity);

        return entity;
    }

    public async Task UpdateAsync(GuardianStorageContractV1 entity)
    {
        entity.UpdatedOn = DateTimeOffset.UtcNow;

        string sql = "UPDATE dbo." + tableName + " SET Prefix = @Prefix, FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, Suffix = @Suffix, " +
            "CellPhoneNumber = @CellPhoneNumber, EmailAddress = @EmailAddress, AddressLine1 = @AddressLine1, AddressLine2 = @AddressLine2, City = @City, State = @State, " +
            "ZipCode = @ZipCode, ReceiveUpdates = @ReceiveUpdates, ContractVersion = @ContractVersion WHERE Id = @Id;";

        await _db.SaveData(sql, entity);
    }

    public async Task DeleteAsync(string id)
    {
        await Task.Delay(1000);
    }
}

