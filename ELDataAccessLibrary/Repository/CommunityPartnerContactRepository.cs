using System;
using ELDataAccessLibrary.StorageContracts;

namespace ELDataAccessLibrary.Repository;

public class CommunityPartnerContactRepository : IDataRepository<CommunityPartnerContactStorageContractV1>
{
    private readonly ISqlDataAccess _db;

    public CommunityPartnerContactRepository(ISqlDataAccess db)
    {
       _db = db;
    }

    public async Task<IEnumerable<CommunityPartnerContactStorageContractV1>> GetAllAsync()
    {
        string sql = "select * from dbo.CommunityPartnerContact;";
        var data = await _db.LoadData<CommunityPartnerContactStorageContractV1, dynamic>(sql, new { });

        return data;
    }

    public async Task<CommunityPartnerContactStorageContractV1> GetByIdAsync(int id)
    {
        string sql = "select * from dbo.CommunityPartnerContact where Id = @Id;";
        var data = await _db.LoadData<CommunityPartnerContactStorageContractV1, dynamic>(sql, new { });

        return data.FirstOrDefault()!;
    }

    public async Task AddAsync(CommunityPartnerContactStorageContractV1 entity)
    {
        string sql = "insert into dbo.CommunityPartnerContact (Prefix, FirstName, MiddleName, LastName, Suffix, OfficePhoneNumber, CellPhoneNumber, EmailAddress, AddressLine1, AddressLine2, City, State, ZipCode, CommunityPartnerName) " +
        "values (@Prefix, @FirstName, @MiddleName, @LastName, @Suffix, @OfficePhoneNumber, @CellPhoneNumber, @EmailAddress, @AddressLine1, @AddressLine2, @City, @State, @ZipCode, @CommunityPartnerName);";

        await _db.SaveData(sql, entity);
    }
    public async Task UpdateAsync(CommunityPartnerContactStorageContractV1 entity)
    {
        await Task.Delay(1000);
    }

    public async Task DeleteAsync(int id)
    {
        await Task.Delay(1000);
    }
}

