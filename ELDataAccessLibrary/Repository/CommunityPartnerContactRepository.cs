using System;
using ELDataAccessLibrary.StorageContracts;

namespace ELDataAccessLibrary.Repository;

public class CommunityPartnerContactRepository : RepositoryBase, IDataRepository<CommunityPartnerContactStorageContractV1>
{
    private readonly ISqlDataAccess _db;
    private readonly string tableName = "CommunityPartnerContact";

    public CommunityPartnerContactRepository(ISqlDataAccess db)
    {
       _db = db;
    }

    public async Task<IEnumerable<CommunityPartnerContactStorageContractV1>> GetAllAsync()
    {
        string sql = "select * from dbo." + tableName + ";";
        var data = await _db.LoadData<CommunityPartnerContactStorageContractV1, dynamic>(sql, new { });

        return data;
    }

    public async Task<CommunityPartnerContactStorageContractV1> GetByIdAsync(string id)
    {
        string sql = "select * from dbo." + tableName + " where Id = @Id;";
        var data = await _db.LoadData<CommunityPartnerContactStorageContractV1, dynamic>(sql, new { Id = id });

        return data.FirstOrDefault()!;
    }

    public async Task<CommunityPartnerContactStorageContractV1> AddAsync(CommunityPartnerContactStorageContractV1 entity)
    {       
        entity.Id = GenerateId(entity.FirstName, entity.LastName);
        
        string sql = "insert into dbo." + tableName + " (Id, Prefix, FirstName, MiddleName, LastName, Suffix, OfficePhoneNumber, CellPhoneNumber, EmailAddress, AddressLine1, AddressLine2, City, State, ZipCode, CommunityPartnerName, ContractVersion) " +
        "values (@Id, @Prefix, @FirstName, @MiddleName, @LastName, @Suffix, @OfficePhoneNumber, @CellPhoneNumber, @EmailAddress, @AddressLine1, @AddressLine2, @City, @State, @ZipCode, @CommunityPartnerName, @ContractVersion);";

        await _db.SaveData(sql, entity);

        return entity;
    }
    public async Task UpdateAsync(CommunityPartnerContactStorageContractV1 entity)
    {
        entity.UpdatedOn = DateTimeOffset.UtcNow;
        await Task.Delay(1000);    
    }

    public async Task DeleteAsync(string id)
    {
        await Task.Delay(1000);
    }
}

