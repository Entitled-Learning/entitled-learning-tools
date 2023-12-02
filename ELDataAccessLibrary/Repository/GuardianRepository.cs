using System;
using ELDataAccessLibrary.StorageContracts;

namespace ELDataAccessLibrary.Repository;

public class GuardianRepository : IDataRepository<GuardianStorageContractV1>
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

    public async Task<GuardianStorageContractV1> GetByIdAsync(int id)
    {
        string sql = "select * from dbo." + tableName + " where Id = @Id;";
        var data = await _db.LoadData<GuardianStorageContractV1, dynamic>(sql, new { });

        return data.FirstOrDefault()!;
    }

    public async Task AddAsync(GuardianStorageContractV1 entity)
    {
        string sql = "insert into dbo." + tableName + " (Id, FirstName, LastName, CellPhoneNumber, EmailAddress, AddressLine1, AddressLine2, City, State, ZipCode) " +
        "values (@Id, @FirstName, @LastName, @CellPhoneNumber, @EmailAddress, @AddressLine1, @AddressLine2, @City, @State, @ZipCode);";

        await _db.SaveData(sql, entity);
    }

    public async Task UpdateAsync(GuardianStorageContractV1 entity)
    {
        entity.UpdatedOn = DateTimeOffset.UtcNow;
        await Task.Delay(1000);
    }

    public async Task DeleteAsync(int id)
    {
        await Task.Delay(1000);
    }
}

