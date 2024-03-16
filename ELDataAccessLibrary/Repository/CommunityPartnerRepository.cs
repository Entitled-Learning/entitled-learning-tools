// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using ELDataAccessLibrary.StorageContracts;

namespace ELDataAccessLibrary.Repository;

public class CommunityPartnerRepository : IDataRepository<CommunityPartnerStorageContractV1>
{
    private readonly ISqlDataAccess _db;
    private readonly string tableName = "CommunityPartner";

    public CommunityPartnerRepository(ISqlDataAccess db)
    {
       _db = db;
    }

    public async Task<IEnumerable<CommunityPartnerStorageContractV1>> GetAllAsync()
    {
        string sql = "select * from dbo." + tableName + ";";
        var data = await _db.LoadData<CommunityPartnerStorageContractV1, dynamic>(sql, new { });

        return data;
    }

    public async Task<CommunityPartnerStorageContractV1> GetByIdAsync(string name)
    {
        string sql = "select * from dbo." + tableName + " where Name = @name;";
        var data = await _db.LoadData<CommunityPartnerStorageContractV1, dynamic>(sql, new { Name = name });

        return data.FirstOrDefault()!;
    }

    public async Task<CommunityPartnerStorageContractV1> AddAsync(CommunityPartnerStorageContractV1 entity)
    {
        string sql = "insert into dbo." + tableName + " (Name, PhoneNumber, EmailAddress, AddressLine1, AddressLine2, City, State, ZipCode, ContractVersion) " +
        "values (@Name, @PhoneNumber, @EmailAddress, @AddressLine1, @AddressLine2, @City, @State, @ZipCode, @ContractVersion);";

        await _db.SaveData(sql, entity);

        return entity;
    }

    public async Task UpdateAsync(CommunityPartnerStorageContractV1 entity)
    {
        entity.UpdatedOn = DateTimeOffset.UtcNow;
        await Task.Delay(1000); 
    }

    public async Task DeleteAsync(string id)
    {
        await Task.Delay(1000);
    }
}

