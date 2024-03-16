// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using Microsoft.Extensions.Logging;
using ELDataAccessLibrary.StorageContracts;

namespace ELDataAccessLibrary.Repository;

public class CommunityPartnerRepository : IDataRepository<CommunityPartnerStorageContractV1>
{
    private readonly string tableName = "CommunityPartner";
    private readonly ISqlDataAccess _db;
    private readonly ILogger<CommunityPartnerRepository> _logger;

    public CommunityPartnerRepository(ISqlDataAccess db, ILogger<CommunityPartnerRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<IEnumerable<CommunityPartnerStorageContractV1>> GetAllAsync()
    {
        string sql = "select * from dbo." + tableName + ";";

        try {
            var data = await _db.LoadData<CommunityPartnerStorageContractV1, dynamic>(sql, new { });
            return data;
        } catch (Exception ex) {
            _logger.GetCommunityPartnerError(ex);
            throw;
        }
    }

    public async Task<CommunityPartnerStorageContractV1> GetByIdAsync(string name)
    {
        string sql = "select * from dbo." + tableName + " where Name = @name;";

        try{
            var data = await _db.LoadData<CommunityPartnerStorageContractV1, dynamic>(sql, new { Name = name });
            return data.FirstOrDefault()!;
        } catch (Exception ex) {
            _logger.GetCommunityPartnerError(ex);
            throw;
        }
    }

    public async Task<CommunityPartnerStorageContractV1> AddAsync(CommunityPartnerStorageContractV1 entity)
    {
        string sql = "insert into dbo." + tableName + " (Name, PhoneNumber, EmailAddress, AddressLine1, AddressLine2, City, State, ZipCode, ContractVersion) " +
        "values (@Name, @PhoneNumber, @EmailAddress, @AddressLine1, @AddressLine2, @City, @State, @ZipCode, @ContractVersion);";

        try{
            await _db.SaveData(sql, entity);
            return entity;
        } catch (Exception ex) {
            _logger.CreateCommunityPartnerError(ex);
            throw;
        }
    }

    public async Task UpdateAsync(CommunityPartnerStorageContractV1 entity)
    {
        entity.UpdatedOn = DateTimeOffset.UtcNow;

        try{
            string sql = "update dbo." + tableName + " set PhoneNumber = @PhoneNumber, EmailAddress = @EmailAddress, AddressLine1 = @AddressLine1, AddressLine2 = @AddressLine2, City = @City, State = @State, ZipCode = @ZipCode, ContractVersion = @ContractVersion, UpdatedOn = @UpdatedOn where Name = @Name;";
            await _db.SaveData(sql, entity);
        } catch (Exception ex) {
            _logger.UpdateCommunityPartnerError(ex);
            throw;
        }
    }

    public async Task DeleteAsync(string id)
    {
        try{
            string sql = "delete from dbo." + tableName + " where Name = @Name;";
            await _db.SaveData(sql, new { Name = id });
        } catch (Exception ex) {
            _logger.DeleteCommunityPartnerError(ex);
            throw;
        }
    }
}

